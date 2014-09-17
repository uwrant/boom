using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    public class BacklogsController : Controller
    {
        private BoomContext boomContext;

        public BacklogsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /backlogs/
        public IActionResult Get()
        {
            var backlogs = boomContext.Backlogs.ToList();
            return this.Json(backlogs);
        }

        // GET: /backlogs/{id}
        public IActionResult Get(long id)
        {
            var backlog = boomContext.Backlogs.SingleOrDefault(b => b.Id == id);
            if (backlog != null)
            {
                return this.Json(backlog);
            }
            else
            {
                return this.HttpNotFound();
            }
        }

        // PUT: /backlogs/{id}
        public IActionResult Put(long id, [FromBody] Backlog backlog)
        {
            // Backlog update = JsonConvert.DeserializeObject<Backlog>(backlog);
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }

        // POST: /backlogs/{id}
        // BODY: {"name":"backlogName"}
        public IActionResult Post([FromBody] Backlog backlog)
        {
            // TODO: unique name
            // TODO: read input

            backlog.Options = new List<BacklogOption>();
            
            this.boomContext.Add(backlog);
            this.boomContext.SaveChanges();

            return Json(backlog);
        }

        // DELETE: /backlogs/{id}
        public IActionResult Delete(long id)
        {
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
