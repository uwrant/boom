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
            return this.Json(new {
                Id = 0,
                Name = "Team Presentation",
                NumberOfAllowedVotes = 3,
                Options = new[] {
                        new { Id = 0, Name = "Pizza" },
                        new { Id = 1, Name = "Pasta" },
                        new { Id = 2, Name = "Pommes" },
                        new { Id = 3, Name = "Schoggi" },
                        new { Id = 4, Name = "Fleisch" }
                    } });
        }
    }
}
