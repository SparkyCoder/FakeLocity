namespace FakeLocity.Installers
{
    using System;
    using System.Configuration;
    using System.Web.Configuration;
    using System.Web.Http;
    using Castle.DynamicProxy;
    using Castle.Facilities.Logging;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Interceptors;
    using Models.Commands;
    using Models.DAL;
    using Models.Factories;
    using Models.Helpers;
    using Models.Queries;

    public class DependencyInstaller : IWindsorInstaller
    {
        private const string NlogConfigPath = "./NLog.config";
        private const string DatabaseConnection = "Database";

        private string GetConfigValue(string key)
        {
           return WebConfigurationManager.AppSettings[key];
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DatabaseConnection].ConnectionString;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<LoggingFacility>(
                facility => facility.UseNLog().LogUsing(LoggerImplementation.NLog).WithConfig(NlogConfigPath));

            container.Register(
                Component.For<IInterceptor>().ImplementedBy<ExceptionInterceptor>(),
                Component.For<ICommandFactory>().AsFactory(),
                Component.For<IQueryFactory>().AsFactory(),

                Classes.FromThisAssembly().BasedOn<ICommand>().WithServiceSelf().LifestylePerWebRequest(),
                Classes.FromThisAssembly().BasedOn<IQuery>().WithServiceSelf().LifestylePerWebRequest(),

                Component.For<IPayrollDetailsHelper>()
                    .ImplementedBy<PayrollDetailsHelper>()
                    .DependsOn(Dependency.OnValue("benefitsDeduction", Convert.ToInt32(GetConfigValue("benefitsDeduction"))))
                    .DependsOn(Dependency.OnValue("dependentDeductions", Convert.ToInt32(GetConfigValue("dependentDeductions"))))
                    .DependsOn(Dependency.OnValue("savingsPercentage", Convert.ToInt32(GetConfigValue("SavingsPercentage"))))
                    .LifestylePerWebRequest(),

                Component.For<IDapperHub>()
                    .ImplementedBy<DapperHub>()
                    .DependsOn(Dependency.OnValue("databaseConnection", GetConnectionString()))
                    .LifestylePerWebRequest(),
                
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient().Configure(registration => registration.Interceptors<ExceptionInterceptor>()));
        }
    }
}