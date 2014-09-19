using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [ApplicationJsonHeader]
    public class SurveyResultController : BoomController
    {
        // GET: /surveys/{surveyId}/result
        public IActionResult Get(long surveyId)
        {
            var options = new[]
            {
                new { Description = "Kart fahren", Count = 12 },
                new { Description = "Eis essenn", Count = 7 },
                new { Description = "Grillebn", Count = 2 },
                new { Description = "Lasertag", Count = 19 },
                new { Description = "Klettern", Count = 13 }
            };

            return this.JsonSerialized(options);
        }
    }
}
