using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Models.Results;

namespace NBlackout.MoneyManager.Bll.DataManagement
{
    public class DataManagementManager : IDisposable
    {
        private MoneyManagerDbContext context = new MoneyManagerDbContext();

        public async Task<long> ImportAsync(string username, string fileName, byte[] content)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var beginDate = DateTime.Now;
            var result = await ImportOfx(content);
            var endDate = DateTime.Now;

            var accounts = result.Accounts;
            var transactions = result.Transactions;
            if (accounts.Any() || transactions.Any())
            {
                var importHistory = new ImportHistoryModel
                {
                    FileName = fileName,
                    BeginDate = beginDate,
                    UserName = username,
                    EndDate = endDate,
                    Accounts = accounts,
                    Transactions = transactions
                };

                context.ImportHistories.Add(importHistory);
                await context.SaveChangesAsync();

                return transactions.Count;
            }

            return 0;
        }

        private async Task<ImportResult> ImportOfx(byte[] content)
        {
            var result = new ImportResult();

            var dateFormat = "yyyyMMdd";
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var balanceFound = false;

            var amountPattern = "(?<amount>[+-]?[0-9]+[\\.][0-9]+)";
            var datePattern = "(?<date>[0-9]{2}[0-9]{2}[0-9]{4})";
            var labelPattern = "(?<label>[^<]+)";

            var accountBeginRegex = new Regex("<STMTTRNRS>");
            var accountNumberRegex = new Regex("<ACCTID>" + labelPattern);
            var accountBalanceRegex = new Regex("<BALAMT>" + amountPattern);
            var accountEndRegex = new Regex("</STMTTRNRS>");

            var transactionBeginRegex = new Regex("<STMTTRN>");
            var transactionDateRegex = new Regex("<DTPOSTED>" + datePattern);
            var transactionAmountRegex = new Regex("<TRNAMT>" + amountPattern);
            var transactionNameRegex = new Regex("<NAME>" + labelPattern);
            var transactionMemoRegex = new Regex("<MEMO>" + labelPattern);
            var transactionNumberRegex = new Regex("<FITID>" + labelPattern);
            var transactionEndRegex = new Regex("</STMTTRN>");

            var transactionNumbers = await context.Transactions.Select(t => t.Number).ToListAsync();
            var accounts = await context.Accounts.ToListAsync();

            using (var reader = new StreamReader(new MemoryStream(content)))
            {
                AccountModel account = null;
                TransactionModel transaction = null;
                IEnumerable<AutomationElementModel> automationElements = null;
                IEnumerable<TransactionForecastHitModel> hits = null;

                for (var line = String.Empty; line != null; line = await reader.ReadLineAsync())
                {
                    // Account begin
                    var match = accountBeginRegex.Match(line);
                    if (match.Success)
                    {
                        account = null;

                        continue;
                    }

                    // Account number
                    match = accountNumberRegex.Match(line);
                    if (match.Success)
                    {
                        var technicalId = match.Groups["label"].Value;
                        account = accounts.SingleOrDefault(m => m.TechnicalId == technicalId);

                        if (account == null)
                        {
                            account = new AccountModel { Number = technicalId, Title = technicalId, TechnicalId = technicalId, Enabled = true };

                            context.Accounts.Add(account);
                            result.Accounts.Add(account);
                        }

                        automationElements = await context.AutomationElements.Where(t => t.Account.TechnicalId == account.TechnicalId).ToListAsync();
                        hits = await context.TransactionForecastHits.Where(t => t.Forecast.Account.TechnicalId == account.TechnicalId && !t.TransactionId.HasValue).ToListAsync();

                        continue;
                    }

                    // Account balance
                    match = accountBalanceRegex.Match(line);
                    if (match.Success)
                    {
                        var sBalance = match.Groups["amount"].Value;
                        var balance = Decimal.Parse(sBalance, numberFormatInfo);

                        if (balance != 0m)
                        {
                            account.Balance = balance;
                            balanceFound = true;
                        }

                        account.LastSync = DateTime.Now;

                        await context.SaveChangesAsync();

                        continue;
                    }

                    // Account end
                    match = accountEndRegex.Match(line);
                    if (match.Success)
                    {
                        account = null;
                        balanceFound = false;

                        continue;
                    }

                    // Transaction begin
                    match = transactionBeginRegex.Match(line);
                    if (match.Success)
                    {
                        transaction = new TransactionModel { AccountId = account.Id };

                        continue;
                    }

                    // Transaction date
                    match = transactionDateRegex.Match(line);
                    if (match.Success)
                    {
                        var sDate = match.Groups["date"].Value;
                        var date = DateTime.ParseExact(sDate, dateFormat, CultureInfo.CurrentCulture);

                        transaction.Date = date;

                        continue;
                    }

                    // Transaction amount
                    match = transactionAmountRegex.Match(line);
                    if (match.Success)
                    {
                        var sAmount = match.Groups["amount"].Value;
                        var amount = Decimal.Parse(sAmount, numberFormatInfo);

                        transaction.Amount = amount;

                        continue;
                    }

                    // Transaction name
                    match = transactionNameRegex.Match(line);
                    if (match.Success)
                    {
                        var label = match.Groups["label"].Value;

                        transaction.Label = Regex.Replace(label, "\\s+", " ");

                        continue;
                    }

                    // Transaction memo
                    match = transactionMemoRegex.Match(line);
                    if (match.Success)
                    {
                        var memo = match.Groups["label"].Value;
                        memo = Regex.Replace(memo, "\\s+", " ");

                        transaction.Label = String.Concat(transaction.Label, " ", memo);

                        continue;
                    }

                    // Transaction number
                    match = transactionNumberRegex.Match(line);
                    if (match.Success)
                    {
                        var number = match.Groups["label"].Value;
                        if (number.Contains(":"))
                            number = number.Split(':')[0];

                        transaction.Number = number;

                        continue;
                    }

                    // Transaction end
                    match = transactionEndRegex.Match(line);
                    if (match.Success)
                    {
                        var exists = transactionNumbers.Any(m => m == transaction.Number);
                        if (!exists)
                        {
                            if (!balanceFound)
                                account.Balance += transaction.Amount;

                            var matchingAutomationElements = automationElements.Where(t => t.Tags.Any(m => transaction.Label.Contains(m.Label)));
                            if (matchingAutomationElements.Count() == 1)
                            {
                                transaction.OrganizationId = matchingAutomationElements.First().OrganizationId;

                                var matchingHits = hits.Where(t => t.Date.Year == transaction.Date.Year && t.Date.Month == transaction.Date.Month);
                                if (matchingHits.Count() == 1)
                                    matchingHits.First().Transaction = transaction;
                            }

                            context.Transactions.Add(transaction);
                            result.Transactions.Add(transaction);

                            transaction = null;
                        }

                        continue;
                    }
                }

                reader.Close();
            }

            await context.SaveChangesAsync();

            return result;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        #endregion
    }
}
