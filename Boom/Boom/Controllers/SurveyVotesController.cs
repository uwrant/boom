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
        // BODY: {"Participant":{"Id":"6"}, "Options":[ {"Option":{"Id":"3", "Description":"hello"}, "Weight":"2" }] }
        public IActionResult Post(long surveyId, [FromBody] Vote vote)
        {
            var participant = boomContext.Participants.SingleOrDefault(p => p.SurveyId == surveyId && p.Id == vote.Participant.Id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            vote.Participant = participant;

            foreach (SurveyOptionVote optionVote in vote.Options) {
                var persistedOption = boomContext.SurveyOptions.SingleOrDefault(o => o.Id == optionVote.Option.Id);
                if (persistedOption == null)
                {
                    return HttpNotFound();
                }
                optionVote.Option = persistedOption;
                boomContext.Add(optionVote);
            }

            boomContext.Add(vote);
            boomContext.SaveChanges();
            return this.Json(vote);
        }
    }
}
