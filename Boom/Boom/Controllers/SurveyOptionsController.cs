using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    [ApplicationJsonHeader]
    public class SurveyOptionsController : BoomController
    {
        private BoomContext boomContext;

        public SurveyOptionsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/{surveyId}/options
        public IActionResult Get(long surveyId)
        {
            var options = this.boomContext.SurveyOptions.Where(o => o.SurveyId == surveyId).ToList();
            return this.JsonSerialized(options);
        }

        // GET: /surveys/{surveyId}/options/{id}
        public IActionResult Get(long surveyId, long id)
        {
            var option = this.boomContext.SurveyOptions.SingleOrDefault(o => o.Id == id && o.SurveyId == surveyId);
            if (option == null)
            {
                return this.HttpNotFound();
            }
            return this.JsonSerialized(option);
        }

        // POST:  /surveys/{surveyId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Post(long surveyId, [FromBody] SurveyOption option)
        {
            var survey = boomContext.Surveys.SingleOrDefault(s => s.Id == surveyId);
            if (survey == null)
            {
                return HttpNotFound();
            }

            boomContext.Add(option);

            survey.Options.Add(option);
            option.Survey = survey;
            option.SurveyId = surveyId;

            boomContext.SaveChanges();
            return this.JsonSerialized(option);
        }

        // PUT: /surveys/{surveyId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Put(long surveyId, long id, [FromBody] SurveyOption option)
        {
            var persistedOption = this.boomContext.SurveyOptions.SingleOrDefault(o => o.Id == id && o.SurveyId == surveyId);
            if (persistedOption == null)
            {
                return this.HttpNotFound();
            }

            persistedOption.Description = option.Description;

            this.boomContext.SaveChanges();
            return this.JsonSerialized(option);
        }

        // DELETE: /surveys/{surveyId}/options/{id}
        public IActionResult Delete(long surveyId, long id)
        {
            var option = this.boomContext.SurveyOptions.SingleOrDefault(o => o.Id == id && o.SurveyId == surveyId);
            if (option == null)
            {
                return this.HttpNotFound();
            }
            this.boomContext.Delete(option);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
