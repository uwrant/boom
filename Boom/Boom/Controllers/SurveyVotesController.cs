using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class SurveyVotesController : Controller
    {
        private BoomContext boomContext;

        public SurveyVotesController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/{surveyId}/votes
        public IActionResult Get(long surveyId)
        {
            var options = this.boomContext.Votes.Where(v => v.Participant.SurveyId == surveyId).ToList();
            return this.Json(options);
        }

        // POST:  /surveys/{surveyId}/votes
        // BODY: {"Participant":{"Id":"6"}, "Options":[ {"Id":"3", "Description":"hello"}:"2", {"Id":"7", "Description":"world"}:"4"] }
        public IActionResult Post(long surveyId, [FromBody] Vote vote)
        {
            var participant = boomContext.Participants.SingleOrDefault(p => p.Survey.Id == surveyId && p.Id == vote.Participant.Id);
            if (participant == null)
            {
                return HttpNotFound();
            }

            boomContext.Add(vote);

            vote.Participant = participant;

            // TODO add options or are they already added

            boomContext.SaveChanges();
            return this.Json(vote);
        }
    }
}
