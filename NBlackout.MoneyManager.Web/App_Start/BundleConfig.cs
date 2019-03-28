using System;
using System.Web.Optimization;

namespace NBlackout.MoneyManager.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
                throw new ArgumentNullException(nameof(bundles));

            RegisterBaseBundles(bundles);
            RegisterFrameworkBundles(bundles);
            RegisterMoneyManagerBundles(bundles);
        }

        private static void RegisterBaseBundles(BundleCollection bundles)
        {
            // Bootstrap
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap/Bundle").Include(
                "~/Scripts/bootstrap/bootstrap.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Styles/Bootstrap/Bundle").Include(
                "~/Content/Styles/bootstrap/bootstrap.css",
                "~/Content/Styles/bootstrap/bootstrap.css.map"
            ));

            // Highcharts
            bundles.Add(new ScriptBundle("~/Scripts/Highcharts/Bundle").Include(
                "~/Scripts/highcharts/highcharts.js"
            ));

            // jQuery
            bundles.Add(new ScriptBundle("~/Scripts/jQuery/Bundle").Include(
                "~/Scripts/jquery/jquery-{version}.js"
            ));

            // jQuery-Noty
            bundles.Add(new ScriptBundle("~/Scripts/jQuery-Noty/Bundle").Include(
                "~/Scripts/jquery-noty/jquery-noty-{version}.js"
            ));

            // jQuery-UI
            bundles.Add(new ScriptBundle("~/Scripts/jQuery-UI/Bundle").Include(
                "~/Scripts/jquery-ui/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui/jquery-ui-datepicker-fr.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Styles/jQuery-UI/Bundle").Include(
                "~/Content/Styles/jquery-ui/jquery-ui.css"
            ));

            // Tagify
            bundles.Add(new ScriptBundle("~/Scripts/Tagify/Bundle").Include(
                "~/Scripts/tagify/jQuery.tagify.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Styles/Tagify/Bundle").Include(
                "~/Content/Styles/tagify/tagify.css"
            ));
        }

        private static void RegisterFrameworkBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/Bundle").Include(
                "~/Scripts/nblackout/nblackout.js",
                "~/Scripts/nblackout/nblackout.core.js",
                "~/Scripts/nblackout/nblackout.ui.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Styles/NBlackout/Bundle").Include(
                "~/Content/Styles/nblackout/nblackout.css"
            ));
        }

        private static void RegisterMoneyManagerBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Bundle").Include(
                   "~/Scripts/nblackout/money-manager/money-manager.js"
               ));

            bundles.Add(new StyleBundle("~/Content/Styles/NBlackout/MoneyManager/Bundle").Include(
                "~/Content/Styles/nblackout/money-manager/money-manager.css"
            ));

            RegisterAdministrationBundles(bundles);
            RegisterConfigurationBundles(bundles);
            RegisterConsultationBundles(bundles);
            RegisterSecurityBundles(bundles);
        }

        private static void RegisterAdministrationBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/administration.js"
            ));

            // Account categories
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/AccountCategories/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/accountCategories/accountCategories.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/AccountCategories/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/accountCategories/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/AccountCategories/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/accountCategories/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/AccountCategories/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/accountCategories/listPartial.js"
            ));

            // Organization categories
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/OrganizationCategories/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/organizationCategories/organizationCategories.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/OrganizationCategories/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/organizationCategories/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/OrganizationCategories/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/organizationCategories/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/OrganizationCategories/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/organizationCategories/listPartial.js"
            ));

            // Customers
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/Customers/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/customers/customers.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/Customers/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/customers/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/Customers/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/customers/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Administration/Customers/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/administration/customers/listPartial.js"
            ));
        }

        private static void RegisterConfigurationBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/configuration.js"
            ));

            // Accounts
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Accounts/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/accounts/accounts.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Accounts/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/accounts/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Accounts/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/accounts/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Accounts/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/accounts/listPartial.js"
            ));

            // DataManagement
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/DataManagement/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/dataManagement/dataManagement.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/DataManagement/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/dataManagement/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/DataManagement/ImportHistories/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/dataManagement/importHistories.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/DataManagement/ImportHistoriesPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/dataManagement/importHistoriesPartial.js"
            ));

            // Organizations
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Organizations/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/organizations/organizations.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Organizations/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/organizations/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Organizations/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/organizations/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Organizations/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/organizations/listPartial.js"
            ));

            // Planning
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/planning.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/Suggestions/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/suggestions.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/SuggestionsPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/suggestionsPartial.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/ForecastHits/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/forecastHits.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Configuration/Planning/ForecastHitsPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/configuration/planning/forecastHitsPartial.js"
            ));
        }

        private static void RegisterConsultationBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/consultation.js"
            ));

            // Accounts
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/accounts.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/list.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/ListPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/listPartial.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/ActivityPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/activityPartial.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Accounts/TransactionForecastHitsOfMonthPartial/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/accounts/transactionForecastHitsOfMonthPartial.js"
            ));

            // Dashboard
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Dashboard/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/dashboard/dashboard.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Dashboard/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/dashboard/index.js"
            ));

            // Transactions
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Transactions/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/transactions/transactions.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Transactions/Index/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/transactions/index.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Consultation/Transactions/List/Bundle").Include(
                "~/Scripts/nblackout/money-manager/consultation/transactions/list.js"
            ));
        }

        private static void RegisterSecurityBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Security/Bundle").Include(
                "~/Scripts/nblackout/money-manager/security/security.js"
            ));

            // User
            bundles.Add(new ScriptBundle("~/Scripts/NBlackout/MoneyManager/Security/User/Bundle").Include(
                "~/Scripts/nblackout/money-manager/security/user/user.js"
            ));

        }
    }
}