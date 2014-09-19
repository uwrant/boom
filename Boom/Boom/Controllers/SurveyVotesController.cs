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
    public class SurveyVotesController : BoomController
    {
        private BoomContext boomContext;

        public SurveyVotesController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/{surveyId}/votes
        public IActionResult Get(long surveyId)
        {
            var options = this.boomContext.Votes
                .Where(v => v.Participant.Survey.Id == surveyId)
                .Include(v => v.Options)
                .Include(v => v.Participant)
                .ToList();

            return this.JsonSerialized(options);
        }

        // POST:  /surveys/{surveyId}/votes
        // BODY: {"Participant":{"Id":"6"}, "Options":[ {"Id":"3"}, {"Id":"4"}] }
        public IActionResult Post(long surveyId, [FromBody] Vote vote)
        {
            var participant = boomContext.Participants.SingleOrDefault(p => p.Survey.Id == surveyId && p.Id == vote.Participant.Id);

            if (participant == null)
            {
                return HttpNotFound();
            }
            vote.Participant = participant;

            var options = new List<SurveyOption>();
            foreach (SurveyOption optionVote in vote.Options) {
                var persistedOption = boomContext.SurveyOptions.SingleOrDefault(o => o.Id == optionVote.Id);
                if (persistedOption == null)
                {
                    return HttpNotFound();
                }
                options.Add(persistedOption);
            }
            vote.Options = options;

            boomContext.Votes.Add(vote);
            boomContext.SaveChanges();
            return this.JsonSerialized(vote);
        }
    }
}
