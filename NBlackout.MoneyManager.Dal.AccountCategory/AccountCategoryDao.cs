using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.AccountCategory
{
    public class AccountCategoryDao : BaseDao<AccountCategoryModel>
    {
        public async Task<bool> ReassignAccountsCategory(long sourceCategoryId, long? targetCategoryId)
        {
            var sourceCategory = await FindByIdAsync(sourceCategoryId);
            if (sourceCategory == null)
                return false;

            if (targetCategoryId.HasValue)
            {
                var targetCategory = await FindByIdAsync(targetCategoryId.Value);
                if (targetCategory == null)
                    return false;
            }

            var accounts = context.Accounts.Where(t => t.CategoryId == sourceCategoryId);
            foreach (var account in accounts)
            {
                account.CategoryId = targetCategoryId;
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
