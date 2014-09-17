using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

// TODO: routing
namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class BacklogOptionsController : Controller
    {
        private BoomContext boomContext;

        public BacklogOptionsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /backlogs/{backlogId}/options
        public IActionResult Get(long backlogId)
        {
            var options = this.boomContext.BacklogOptions.Where(o => o.Backlog.Id == backlogId);
            return this.Json(options);
        }

        // GET: /backlogs/{backlogId}/options/{id}
        public IActionResult Get(long backlogId, long id)
        {
            var option = this.boomContext.BacklogOptions.SingleOrDefault(o => o.Id == id);
            // TODO assert backlogId
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
