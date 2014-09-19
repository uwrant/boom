using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [ApplicationJsonHeader]
    public class SurveyResultsController : BoomController
    {
        private BoomContext boomContext;

        public SurveyResultsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/{surveyId}/result
        public IActionResult Get(long surveyId)
        {
            var survey = this.boomContext.Surveys.Include(t => t.Participants).Include(t => t.Options).SingleOrDefault(t => t.Id == surveyId);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (SurveyOption option in survey.Options)
            {
                result.Add(option.Description, 0);
            }

            List<Vote> votes = new List<Vote>();

            foreach (var participant in survey.Participants)
            {
                votes.AddRange(this.boomContext.Votes.Where(p => p.Participant.Id == participant.Id));
            }

            foreach (Vote vote in votes)
            {
                foreach (SurveyOption option in vote.Options)
                {
                    if (!result.ContainsKey(option.Description))
                    {
                        result.Add(option.Description, 0);
                    }

                    result[option.Description] = result[option.Description] + 1;
                }
            }

            return this.JsonSerialized(result.Select(r => new { Description = r.Key, Count = r.Value }));
        }
    }
}