using System.Data.Entity;
using NBlackout.MoneyManager.Entity.Migrations;

namespace NBlackout.MoneyManager.Entity
{
    public class MoneyManagerDatabaseInitializer : MigrateDatabaseToLatestVersion<MoneyManagerDbContext, Configuration>
    {
    }
}