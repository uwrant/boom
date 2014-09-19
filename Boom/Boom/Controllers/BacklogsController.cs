using Boom.Domain;
using Microsoft.AspNet.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    [ApplicationJsonHeader]
    public class BacklogsController : BoomController
    {
        private BoomContext boomContext;

        public BacklogsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /backlogs/
        public IActionResult Get()
        {
            var backlogs = boomContext.Backlogs
                .Include(b => b.Options)
                .ToList();

            return this.JsonSerialized(backlogs);
        }

        // GET: /backlogs/{id}
        public IActionResult Get(long id)
        {
            var backlog = boomContext.Backlogs
                .Include(b => b.Options)
                .SingleOrDefault(b => b.Id == id);

            if (backlog != null)
            {
                return this.JsonSerialized(backlog);
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
            var persistedBacklog = this.boomContext.Backlogs
                .Include(b => b.Options)
                .SingleOrDefault(b => b.Id == id);

            persistedBacklog.Name = backlog.Name;
            this.boomContext.SaveChanges();

            return this.JsonSerialized(persistedBacklog);
        }

        // POST: /backlogs/{id}
        // BODY: {"Name":"backlogName"}
        public IActionResult Post([FromBody] Backlog backlog)
        {
            // TODO: unique name

            this.boomContext.Backlogs.Add(backlog);
            this.boomContext.SaveChanges();

            return this.JsonSerialized(backlog);
        }

        // DELETE: /backlogs/{id}
        public IActionResult Delete(long id)
        {
            var backlog = this.boomContext.Backlogs
                .Include(b => b.Options)
                .SingleOrDefault(b => b.Id == id);

            this.boomContext.Backlogs.Remove(backlog);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
