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
            var options = this.boomContext.SurveyOptions
                .Where(o => o.Survey.Id == surveyId)
                .ToList();

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (SurveyOption option in options)
            {
                result.Add(option.Description, 0);
            }

            var votes = this.boomContext.Votes
                .Where(v => v.Participant.Survey.Id == surveyId)
                .Include(v => v.Options)
                .Include(v => v.Participant)
                .ToList();

            foreach (Vote vote in votes)
            {
                foreach (SurveyOption option in vote.Options)
                {
                    int count;
                    result.TryGetValue(option.Description, out count);
                    count++;
                    result.Remove(option.Description);
                    result.Add(option.Description, count);
                }
            }

        //     [
        //    {"Description":"value", "Count":1},
        //{"Description":"value", "Count":1}
        //    ]

            return this.JsonSerialized(result);
        }
    }
}