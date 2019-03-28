using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.OrganizationCategory
{
    public class OrganizationCategoryDao : BaseDao<OrganizationCategoryModel>
    {
        public async Task<bool> ReassignOrganizationsCategory(long sourceCategoryId, long targetCategoryId)
        {
            var sourceCategory = await FindByIdAsync(sourceCategoryId);
            if (sourceCategory == null)
                return false;

            var targetCategory = await FindByIdAsync(targetCategoryId);
            if (targetCategory == null)
                return false;

            var organizations = context.Organizations.Where(t => t.CategoryId == sourceCategoryId);
            foreach (var organization in organizations)
            {
                organization.CategoryId = targetCategoryId;
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
