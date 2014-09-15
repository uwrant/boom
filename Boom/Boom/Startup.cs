using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;

namespace Boom
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
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