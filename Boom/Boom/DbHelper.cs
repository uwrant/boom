using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Boom.Domain;
using System.Linq;

namespace Boom
{
    public class DbHelper
    {
        private const string SURVEY_NAME = "TeamPresentation";

        public static void InitDatabase(IBuilder app)
        {
            using (BoomContext db = app.ApplicationServices.GetService<BoomContext>())
            {
                var existingSurvey = db.Surveys.SingleOrDefault(s => s.Name == SURVEY_NAME);
                if (existingSurvey == null)
                {
                    var TeamPresentationSurvey = new Survey()
                    {
                        Name = SURVEY_NAME,
                        CreationDate = DateTime.Now,
                        StartDate = DateTime.Now
                    };

                    var option1 = new SurveyOption() { Description = "AngularJs", Survey = TeamPresentationSurvey };
                    var option2 = new SurveyOption() { Description = "ASP vNext", Survey = TeamPresentationSurvey };
                    var option3 = new SurveyOption() { Description = "Entity Framework", Survey = TeamPresentationSurvey };
                    var option4 = new SurveyOption() { Description = "Ionic", Survey = TeamPresentationSurvey };
                
                    db.Surveys.Add(TeamPresentationSurvey);

                    db.SurveyOptions.Add(option1);
                    db.SurveyOptions.Add(option2);
                    db.SurveyOptions.Add(option3);
                    db.SurveyOptions.Add(option4);

                    db.SaveChanges();
                }
            }
        }
    }
}