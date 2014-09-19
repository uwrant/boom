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

                    var participant1 = new Participant { Name = "Andreas", Survey = TeamPresentationSurvey };
                    var participant2 = new Participant { Name = "Mathias", Survey = TeamPresentationSurvey };
                    var participant3 = new Participant { Name = "Christian", Survey = TeamPresentationSurvey };
                    var participant4 = new Participant { Name = "Stefan", Survey = TeamPresentationSurvey };

                    var vote1 = new Vote { Participant = participant1, Options = (new[] { option1 }).ToList() };
                    var vote2 = new Vote { Participant = participant1, Options = (new[] { option1, option2 }).ToList() };
                    var vote3 = new Vote { Participant = participant1, Options = (new[] { option1, option3 }).ToList() };
                    var vote4 = new Vote { Participant = participant1, Options = (new[] { option1, option4 }).ToList() };

                    db.Votes.AddRange(new[] { vote1, vote2, vote3, vote4 });

                    db.SaveChanges();
                }
            }
        }
    }
}