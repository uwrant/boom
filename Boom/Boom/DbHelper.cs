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
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1)
                    };

                    db.Surveys.Add(TeamPresentationSurvey);

                    db.SaveChanges();
                }
            }
        }
    }
}