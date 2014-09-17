using Boom.Domain;
using Microsoft.AspNet.Mvc;
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
            return this.Json(surveys);
        }

        // GET: /surveys/?open=true
        public IActionResult Get(bool open)
        {
            var surveys = this.boomContext.Surveys.AsQueryable();

            if (open)
            {
                surveys = surveys.Where(s => s.EndDate < DateTime.Now && s.EndDate > s.StartDate);
            }

            return this.Json(surveys);
        }

        // GET: /surveys/{id}
        public IActionResult Get(long id)
        {
            var survey = this.boomContext.Surveys.SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            return this.Json(survey);
        }
    }
}
