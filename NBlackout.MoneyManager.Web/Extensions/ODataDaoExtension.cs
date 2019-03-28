using System;
using System.Threading.Tasks;
using System.Web.OData;
using NBlackout.MoneyManager.Dal.Common;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Web.OData.Extensions
{
    public static class ODataDaoExtension
    {
        public static async Task<T> PatchAsync<T>(this BaseDao<T> dao, long key, Delta<T> delta) where T : BaseModel
        {
            if (delta == null)
                throw new ArgumentNullException(nameof(delta));

            var existingEntity = await dao.FindByIdAsync(key);
            if (existingEntity != null)
            {
                delta.Patch(existingEntity);

                await dao.context.SaveChangesAsync();
            }

            return existingEntity;
        }
    }
}