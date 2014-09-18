using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class SurveysController : Controller
    {
        private BoomContext boomContext;

        public SurveysController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/
        public IActionResult Get()
        {
            var surveys = this.boomContext.Surveys.ToList();

            foreach (var survey in surveys)
            {
                survey.Options = this.boomContext.SurveyOptions.Where(so => so.SurveyId == survey.Id).ToList();
                survey.Participants = this.boomContext.Participants.Where(p => p.SurveyId == survey.Id).ToList();
            }

            var content = JsonConvert.SerializeObject(surveys);

            return this.Content(content);
        }

        // GET: /surveys/?open=true
        public IActionResult Get(bool open)
        {
            var surveysQuery = this.boomContext.Surveys.AsQueryable();

            if (open)
            {
                surveysQuery = surveysQuery.Where(s => s.EndDate == null);
            }

            var surveys = surveysQuery.ToList();

            foreach (var survey in surveys)
            {
                survey.Options = this.boomContext.SurveyOptions.Where(so => so.SurveyId == survey.Id).ToList();
                survey.Participants = this.boomContext.Participants.Where(p => p.SurveyId == survey.Id).ToList();
            }

            var content = JsonConvert.SerializeObject(surveys);
            return this.Content(content);
        }

        // GET: /surveys/{id}
        public IActionResult Get(long id)
        {
            var survey = this.boomContext.Surveys.SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            var content = JsonConvert.SerializeObject(survey);
            return this.Content(content);
        }

        // POST: /surveys/
        public IActionResult Post([FromBody] Survey survey)
        {
            this.boomContext.Add(survey);

            foreach (var option in survey.Options)
            {
                option.Survey = survey;
                option.SurveyId = survey.Id;
                this.boomContext.Add(option);
            }

            this.boomContext.SaveChanges();

            var content = JsonConvert.SerializeObject(survey);
            return this.Content(content);
        }

        // POST: /surveys/id/participants
        public IActionResult Post(long id, [FromBody] string name)
        {
            var survey = this.boomContext.Surveys.SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            var participant = survey.Participants.SingleOrDefault(p => p.Name == name);

            if (participant != null)
            {
                var participantContent = JsonConvert.SerializeObject(participant);
                return this.Content(participantContent);
            }

            participant = new Participant { Name = name, Survey = survey, SurveyId = survey.Id };
            survey.Participants.Add(participant);

            this.boomContext.Add(participant);
            this.boomContext.SaveChanges();

            var content = JsonConvert.SerializeObject(participant);
            return this.Content(content);
        }
    }
}
