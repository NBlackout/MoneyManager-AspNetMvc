using System;
using System.Linq;
using AutoMapper;
using NBlackout.Framework.Identity.EntityFramework;
using NBlackout.MoneyManager.Models;
using NBlackout.MoneyManager.ViewModels;

namespace NBlackout.MoneyManager.Web
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(ConfigureAutoMapper);
            mapperConfiguration.AssertConfigurationIsValid();

            Mapper = mapperConfiguration.CreateMapper();
        }

        private static void ConfigureAutoMapper(IMapperConfigurationExpression configuration)
        {
            configuration.ForAllMaps((categoryMap, mappingExpression) =>
            {
                mappingExpression.MaxDepth(5);
            });

            configuration.CreateMap<FrameworkUser, UserViewModel>()
                .ForMember(vm => vm.Password, o => o.Ignore());
            configuration.CreateMap<UserViewModel, FrameworkUser>()
                .ForMember(m => m.Id, o => o.Ignore())
                .ForMember(m => m.EmailConfirmed, o => o.Ignore())
                .ForMember(m => m.PasswordHash, o => o.Ignore())
                .ForMember(m => m.SecurityStamp, o => o.Ignore())
                .ForMember(m => m.PhoneNumberConfirmed, o => o.Ignore())
                .ForMember(m => m.TwoFactorEnabled, o => o.Ignore())
                .ForMember(m => m.LockoutEndDateUtc, o => o.Ignore())
                .ForMember(m => m.LockoutEnabled, o => o.Ignore())
                .ForMember(m => m.AccessFailedCount, o => o.Ignore())
                .ForMember(m => m.Roles, o => o.Ignore())
                .ForMember(m => m.Logins, o => o.Ignore())
                .ForMember(m => m.Claims, o => o.Ignore());

            configuration.CreateMap<AccountModel, AccountViewModel>()
                .ForMember(vm => vm.EstimatedBalanceByEndOfMonth, o => o.Ignore())
                .ForMember(vm => vm.Customers, o => o.Ignore())
                .ForMember(vm => vm.Categories, o => o.Ignore());
            configuration.CreateMap<AccountViewModel, AccountModel>()
                .ForMember(vm => vm.ImportHistoryId, o => o.Ignore())
                .ForMember(vm => vm.ImportHistory, o => o.Ignore());

            configuration.CreateMap<TransactionModel, TransactionViewModel>()
                .ForMember(vm => vm.Organizations, o => o.Ignore());
            configuration.CreateMap<TransactionViewModel, TransactionModel>()
                .ForMember(m => m.ImportHistoryId, o => o.Ignore())
                .ForMember(m => m.Account, o => o.Ignore())
                .ForMember(m => m.ImportHistory, o => o.Ignore());

            configuration.CreateMap<AutomationElementModel, AutomationElementViewModel>()
                .ForMember(vm => vm.Organizations, o => o.Ignore())
                .ForMember(vm => vm.Accounts, o => o.Ignore());

            configuration.CreateMap<OrganizationModel, OrganizationViewModel>()
                .ForMember(vm => vm.Categories, o => o.Ignore());

            configuration.CreateMap<TransactionForecastModel, TransactionForecastViewModel>()
                .ForMember(vm => vm.Accounts, o => o.Ignore())
                .ForMember(vm => vm.Organizations, o => o.Ignore());

            configuration.CreateMap<ImportHistoryModel, ImportHistoryViewModel>()
                .ForMember(vm => vm.Duration, o => o.Ignore());

            configuration.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(t => t.DateTime);
            configuration.CreateMap<DateTimeOffset?, DateTime?>().ConvertUsing(t => (t.HasValue) ? t.Value.DateTime : (DateTime?)null);
        }
    }
}
