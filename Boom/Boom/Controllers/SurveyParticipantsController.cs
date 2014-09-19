using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
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
            var participants = this.boomContext.Participants.Where(p => p.SurveyId == surveyId).ToList();
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

            boomContext.Add(participant);

            survey.Participants.Add(participant);
            participant.Survey = survey;
            participant.SurveyId = surveyId;

            boomContext.SaveChanges();
            return this.JsonSerialized(participant);
        }
    }
}
