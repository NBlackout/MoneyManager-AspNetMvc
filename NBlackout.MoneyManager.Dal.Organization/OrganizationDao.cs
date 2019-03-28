using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.Organization
{
    public class OrganizationDao : BaseDao<OrganizationModel>
    {
        public async Task<bool> ReassignTransactionsOrganization(long sourceOrganizationId, long? targetOrganizationId)
        {
            var sourceOrganization = await FindByIdAsync(sourceOrganizationId);
            if (sourceOrganization == null)
                return false;

            if (targetOrganizationId.HasValue)
            {
                var targetOrganization = await FindByIdAsync(targetOrganizationId.Value);
                if (targetOrganization == null)
                    return false;
            }

            var transactions = context.Transactions.Where(t => t.OrganizationId == sourceOrganizationId);
            foreach (var transaction in transactions)
            {
                transaction.OrganizationId = targetOrganizationId;
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
