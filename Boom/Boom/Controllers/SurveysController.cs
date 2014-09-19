using Boom.Domain;
using Microsoft.AspNet.Mvc;
using System.Data.Entity;
using System.Linq;

namespace Boom.Controllers
{
    [ApplicationJsonHeader]
    public class SurveysController : BoomController
    {
        private BoomContext boomContext;

        public SurveysController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/
        public IActionResult Get()
        {
            var surveys = this.boomContext.Surveys
                .Include(s => s.Participants)
                .Include(s => s.Options)
                .ToList();

            foreach (var survey in surveys)
            {
                survey.Options = this.boomContext.SurveyOptions.Where(so => so.Survey.Id == survey.Id).ToList();
                survey.Participants = this.boomContext.Participants.Where(p => p.Survey.Id == survey.Id).ToList();
            }

            return this.JsonSerialized(surveys);
        }

        public IActionResult Get(string participant)
        {
            var surveys = this.boomContext.Surveys
                .Include(s => s.Participants)
                .Include(s => s.Options)
                .Where(s => s.Participants.Any(p => p.Name == participant))
                .ToList();

            //foreach (var survey in surveys)
            //{
            //    survey.Options = this.boomContext.SurveyOptions.Where(so => so.Survey.Id == survey.Id).ToList();
            //    survey.Participants = this.boomContext.Participants.Where(p => p.Survey.Id == survey.Id).ToList();
            //}

            return this.JsonSerialized(surveys);
        }

        // GET: /surveys/?open=true
        public IActionResult Get(bool open)
        {
            var surveysQuery = this.boomContext.Surveys.AsQueryable();

            if (open)
            {
                surveysQuery = surveysQuery
                    .Where(s => s.EndDate == null)
                    .Include(s => s.Participants)
                    .Include(s => s.Options);
            }

            var surveys = surveysQuery.ToList();

            foreach (var survey in surveys)
            {
                survey.Options = this.boomContext.SurveyOptions
                    .Where(so => so.Survey.Id == survey.Id)
                    .Include(s => s.Survey)
                    .ToList();

                survey.Participants = this.boomContext.Participants
                    .Where(p => p.Survey.Id == survey.Id)
                    .Include(p => p.Survey)
                    .ToList();
            }

            return this.JsonSerialized(surveys);
        }

        // GET: /surveys/{id}
        public IActionResult Get(long id)
        {
            var survey = this.boomContext.Surveys
                .Include(s => s.Options)
                .Include(s => s.Participants)
                .SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            return this.JsonSerialized(survey);
        }

        // POST: /surveys/
        public IActionResult Post([FromBody] Survey survey)
        {
            this.boomContext.Surveys.Add(survey);

            foreach (var option in survey.Options)
            {
                option.Id = 0;
                option.Survey = survey;
                this.boomContext.SurveyOptions.Add(option);
            }

            this.boomContext.SaveChanges();

            return this.JsonSerialized(survey);
        }

        // PATCH: /surveys/id
        public IActionResult Patch(long id, [FromBody] Survey survey)
        {
            var startDate = survey.StartDate;
            var endDate = survey.EndDate;

            var surveyEntity = this.boomContext.Surveys.SingleOrDefault(s => s.Id == id);

            if (startDate != null)
            {
                surveyEntity.StartDate = startDate;
            }
            else if (endDate != null)
            {
                surveyEntity.EndDate = endDate;
            }

            this.boomContext.SaveChanges();

            return this.JsonSerialized(survey);
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
                return this.JsonSerialized(participant);
            }

            participant = new Participant { Name = name, Survey = survey };
            survey.Participants.Add(participant);

            this.boomContext.Participants.Add(participant);
            this.boomContext.SaveChanges();

            return this.JsonSerialized(participant);
        }
    }
}
