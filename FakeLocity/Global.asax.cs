namespace FakeLocity
{
    using System.Web;
    using System.Web.Http;
    using Installers;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SetupRoutes();
            InstallWindsor();
        }

        private void InstallWindsor()
        {
            IocInstaller.ConfigureWindsor(GlobalConfiguration.Configuration);
        }

        private void SetupRoutes()
        {
            GlobalConfiguration.Configure(configuration => configuration.MapHttpAttributeRoutes());
        }
    }
}