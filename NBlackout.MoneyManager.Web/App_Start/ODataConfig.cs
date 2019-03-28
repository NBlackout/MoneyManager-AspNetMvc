using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.Models.Results;

namespace NBlackout.MoneyManager.Web.OData
{
    public static class ODataConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var edmModel = GetBuilder().GetEdmModel();

            config.EnableUnqualifiedNameCall(true);
            config.EnableEnumPrefixFree(true);
            config.MapODataServiceRoute("ODataRoute", "odata", edmModel);
        }

        private static ODataConventionModelBuilder GetBuilder()
        {
            var builder = new ODataConventionModelBuilder { ContainerName = "MoneyManagerDbContext", Namespace = "NBlackout.MoneyManager.Web.OData.Service" };

            // Entities
            builder.EntitySet<UserCustomerModel>("UserCustomers");
            builder.EntitySet<CustomerModel>("Customers");
            builder.EntitySet<AccountModel>("Accounts");
            builder.EntitySet<AccountCategoryModel>("AccountCategories");
            builder.EntitySet<TransactionModel>("Transactions");
            builder.EntitySet<AutomationElementModel>("AutomationElements");
            builder.EntitySet<OrganizationModel>("Organizations");
            builder.EntitySet<OrganizationCategoryModel>("OrganizationCategories");
            builder.EntitySet<TransactionForecastModel>("TransactionForecasts");
            builder.EntitySet<TransactionForecastHitModel>("TransactionForecastHits");
            builder.EntitySet<ImportHistoryModel>("ImportHistories");

            // Functions
            var transactionsOfMonthFunction = builder.EntityType<AccountModel>().Function("TransactionsOfMonth").ReturnsCollectionFromEntitySet<TransactionModel>("Transactions");
            transactionsOfMonthFunction.Parameter<int>("year");
            transactionsOfMonthFunction.Parameter<int>("month");

            var transactionsOfOrganizationFunction = builder.EntityType<AccountModel>().Function("TransactionsOfOrganization").ReturnsCollectionFromEntitySet<TransactionModel>("Transactions");
            transactionsOfOrganizationFunction.Parameter<long>("organizationId");

            var transactionForecastHitsOfMonthFunction = builder.EntityType<AccountModel>().Function("TransactionForecastHitsOfMonth").ReturnsCollectionFromEntitySet<TransactionForecastHitModel>("TransactionForecastHits");
            transactionForecastHitsOfMonthFunction.Parameter<int>("year");
            transactionForecastHitsOfMonthFunction.Parameter<int>("month");
            transactionForecastHitsOfMonthFunction.Parameter<bool?>("doneFilter");

            builder.EntityType<AccountModel>().Function("Expenditure").ReturnsCollection<ExpenditureResult>();

            builder.EntityType<AccountModel>().Function("EstimatedBalanceByEndOfMonth").Returns<decimal>();

            builder.EntityType<TransactionModel>().Collection.Function("Suggestions").ReturnsCollection<SuggestionResult>();

            builder.EntityType<TransactionForecastHitModel>().Collection.Function("ReconciliationSuggestions").ReturnsCollection<ReconciliationSuggestionResult>();

            // Actions
            var importAction = builder.Action("Import").Returns<long>();
            importAction.Parameter<string>("fileName");
            importAction.Parameter<byte[]>("content");

            var reassignOrganizationsCategory = builder.EntityType<OrganizationCategoryModel>().Collection.Action("ReassignOrganizationsCategory");
            reassignOrganizationsCategory.Parameter<long>("sourceCategoryId");
            reassignOrganizationsCategory.Parameter<long>("targetCategoryId");

            var reassignTransactionsOrganization = builder.EntityType<OrganizationModel>().Collection.Action("ReassignTransactionsOrganization");
            reassignTransactionsOrganization.Parameter<long>("sourceOrganizationId");
            reassignTransactionsOrganization.Parameter<long?>("targetOrganizationId");

            var reassignAccountCategory = builder.EntityType<AccountCategoryModel>().Collection.Action("ReassignAccountsCategory");
            reassignAccountCategory.Parameter<long>("sourceCategoryId");
            reassignAccountCategory.Parameter<long?>("targetCategoryId");

            var reassignCustomer = builder.EntityType<CustomerModel>().Collection.Action("ReassignAccountsCustomer");
            reassignCustomer.Parameter<long>("sourceCustomerId");
            reassignCustomer.Parameter<long?>("targetCustomerId");

            return builder;
        }
    }
}