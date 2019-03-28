using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.Customer
{
    public class CustomerDao : BaseDao<CustomerModel>
    {
        public async Task<bool> ReassignAccountsCustomer(long sourceCustomerId, long? targetCustomerId)
        {
            var sourceCustomer = await FindByIdAsync(sourceCustomerId);
            if (sourceCustomer == null)
                return false;

            if (targetCustomerId.HasValue)
            {
                var targetCustomer = await FindByIdAsync(targetCustomerId.Value);
                if (targetCustomer == null)
                    return false;
            }

            var accounts = context.Accounts.Where(t => t.CustomerId == sourceCustomerId);
            foreach (var account in accounts)
            {
                account.CustomerId = targetCustomerId;
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
