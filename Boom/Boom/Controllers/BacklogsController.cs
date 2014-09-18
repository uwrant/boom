using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

            var content = JsonConvert.SerializeObject(backlogs);
            return this.Content(content);
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
        // BODY: {"Name":"backlogName"}
        public IActionResult Put(long id, [FromBody] Backlog backlog)
        {
            var persistedBacklog = this.boomContext.Backlogs.SingleOrDefault(b => b.Id == id);
            persistedBacklog.Name = backlog.Name;
            this.boomContext.SaveChanges();
            return this.Json(persistedBacklog);
        }

        // POST: /backlogs/{id}
        // BODY: {"Name":"backlogName"}
        public IActionResult Post([FromBody] Backlog backlog)
        {
            // TODO: unique name

            this.boomContext.Add(backlog);
            this.boomContext.SaveChanges();

            return this.Json(backlog);
        }

        // DELETE: /backlogs/{id}
        public IActionResult Delete(long id)
        {
            var backlog = this.boomContext.Backlogs.SingleOrDefault(b => b.Id == id);
            this.boomContext.Delete(backlog);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
