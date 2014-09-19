using Boom.Domain;
using Microsoft.AspNet.Mvc;
using System.Data.Entity;
using System.Linq;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    [ApplicationJsonHeader]
    public class SurveyParticipantsController : BoomController
    {
        private BoomContext boomContext;

        public SurveyParticipantsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/{surveyId}/participants
        public IActionResult Get(long surveyId)
        {
            var participants = this.boomContext.Participants
                .Where(p => p.Survey.Id == surveyId)
                .Include(p => p.Survey)
                .ToList();
            return this.JsonSerialized(participants);
        }

        // POST:  /surveys/{surveyId}/participants
        // BODY: {"Name":"Participant Name"}
        public IActionResult Post(long surveyId, [FromBody] Participant participant)
        {
            // TODO unique name

            var survey = boomContext.Surveys.SingleOrDefault(s => s.Id == surveyId);
            if (survey == null)
            {
                return HttpNotFound();
            }

            boomContext.Participants.Add(participant);

            survey.Participants.Add(participant);
            participant.Survey = survey;

            boomContext.SaveChanges();
            return this.JsonSerialized(participant);
        }
    }
}
