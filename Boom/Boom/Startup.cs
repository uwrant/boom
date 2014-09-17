using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;

namespace Boom
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("config.json");

            app.UseStaticFiles();

            app.UseServices(services =>
             {
                 services.AddMvc();
             });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("ApiRoute", "{controller}/{id?}");
            });

        }
    }
}