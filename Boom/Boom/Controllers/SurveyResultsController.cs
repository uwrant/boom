using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
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
            var options = this.boomContext.Votes
                .Where(v => v.Participant.Survey.Id == surveyId)
                .Include(v => v.Options)
                .Include(v => v.Participant)
                .ToList();

            return this.JsonSerialized(options);
        }
    }
}