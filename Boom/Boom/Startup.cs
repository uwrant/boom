using System;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using System.Data.Entity;

namespace Boom
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("config.json");
            configuration.AddEnvironmentVariables();

            app.UseStaticFiles();

            app.UseServices(services =>
            {
                services.AddMvc();
                services.AddScoped<BoomContext>();
                services.AddInstance(configuration);
                services.AddSingleton<PushNotificationSender>();
            });

            app.Use((context, next) =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                    context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin", "X-Requested-With", "Content-Type", "Accept" });
                    return next();
                });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "BacklogOptionsRoute",
                    template: "backlogs/{backlogId}/options/{id?}",
                    defaults: new { controller = "BacklogOptions" });

                routes.MapRoute(
                  name: "SurveyOptionsRoute",
                  template: "surveys/{surveyId}/options/{id?}",
                  defaults: new { controller = "SurveyOptions" });

                routes.MapRoute(
                  name: "SurveyParticipantsRoute",
                  template: "surveys/{surveyId}/participants",
                  defaults: new { controller = "SurveyParticipants" });

                routes.MapRoute(
                  name: "SurveyVotesRoute",
                  template: "surveys/{surveyId}/votes",
                  defaults: new { controller = "SurveyVotes" });

                routes.MapRoute(
                    name: "SurveyResultsRoute",
                    template: "surveys/{surveyId}/result",
                    defaults: new { controller = "SurveyResult" });

                routes.MapRoute(
                    name: "OptionsRoute",
                    template: "options/{optionId}",
                    defaults: new { controller = "Options" });

                routes.MapRoute(
                    name: "ApiRoute",
                    template: "{controller}/{id?}");
            });


            Database.SetInitializer<BoomContext>(new DropCreateDatabaseIfModelChanges<BoomContext>());
            DbHelper.InitDatabase(app);
        }
    }
}