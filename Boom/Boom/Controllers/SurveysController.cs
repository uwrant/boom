using Boom.Domain;
using Microsoft.AspNet.Mvc;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class SurveysController : Controller
    {
        // GET: /surveys/
        public IActionResult Get()
        {
            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = 1234;

            return this.Json(new[] { new { Id = 0, Name = "Team Presentation" }, new { Id = 1, Name = "Team Event" } });
        }

        // GET: /surveys/{id}
        public IActionResult Get(long id)
        {
            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = id;

            return this.Json(backlog);
        }
    }
}
