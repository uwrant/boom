using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Net;

// TODO: routing
namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class BacklogOptionsController : Controller
    {
        // GET: /backlogs/{backlogId}/options
        public IActionResult Get()
        {
            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = 1234;

            var option = new BacklogOption();
            option.Description = "TestBacklogOption";
            option.Backlog = backlog;

            backlog.Options = new[] { option };
            return this.Json(backlog.Options);
        }

        // GET: /backlogs/{backlogId}/options/{id}
        public IActionResult Get(long id)
        {
            // TODO assert backlogId

            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = 1234;

            var option = new BacklogOption();
            option.Description = "TestBacklogOption";
            option.Backlog = backlog;

            return this.Json(option);
        }

        // PUT: /backlogs/{backlogId}/options/{id}
        public IActionResult Put(long id, [FromBody] Backlog backlog)
        {
            // TODO assert backlogId
            // Backlog update = JsonConvert.DeserializeObject<Backlog>(backlog);
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }

        // POST:  /backlogs/{backlogId}/options/{id}
        public IActionResult Post([FromBody] Backlog backlog)
        {
            // Backlog update = JsonConvert.DeserializeObject<Backlog>(backlog);
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }

        // DELETE: /backlogs/{backlogId}/options/{id}
        public IActionResult Delete(long id)
        {
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
