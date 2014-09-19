using Boom.Domain;
using Microsoft.AspNet.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    [ApplicationJsonHeader]
    public class BacklogOptionsController : BoomController
    {
        private BoomContext boomContext;

        public BacklogOptionsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /backlogs/{backlogId}/options
        public IActionResult Get(long backlogId)
        {
            var options = this.boomContext.BacklogOptions
                .Include(o => o.Backlog)
                .Where(o => o.Backlog.Id == backlogId)
                .ToList();

            return this.JsonSerialized(options);
        }

        // GET: /backlogs/{backlogId}/options/{id}
        public IActionResult Get(long backlogId, long id)
        {
            var option = this.boomContext.BacklogOptions
                .Include(o => o.Backlog)
                .SingleOrDefault(o => o.Id == id && o.Backlog.Id == backlogId);

            if (option == null)
            {
                return HttpNotFound();
            }
            return this.JsonSerialized(option);
        }

        // POST:  /backlogs/{backlogId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Post(long backlogId, [FromBody] BacklogOption option)
        {

            var backlog = boomContext.Backlogs
                .Include(b => b.Options)
                .SingleOrDefault(b => b.Id == backlogId);

            if (backlog == null)
            {
                return HttpNotFound();
            }

            boomContext.BacklogOptions.Add(option);

            backlog.Options.Add(option);
            option.Backlog = backlog;

            boomContext.SaveChanges();
            return this.JsonSerialized(new
            {
                option.Id,
                option.Description
            });
        }

        // PUT: /backlogs/{backlogId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Put(long backlogId, long id, [FromBody] BacklogOption option)
        {
            var persistedOption = this.boomContext.BacklogOptions
                .Include(o => o.Backlog)
                .SingleOrDefault(o => o.Id == id && o.Backlog.Id == backlogId);

            if (persistedOption == null)
            {
                return HttpNotFound();
            }

            persistedOption.Description = option.Description;

            this.boomContext.SaveChanges();
            return this.JsonSerialized(option);
        }

        // DELETE: /backlogs/{backlogId}/options/{id}
        public IActionResult Delete(long backlogId, long id)
        {
            var option = this.boomContext.BacklogOptions
                .Include(o => o.Backlog)
                .SingleOrDefault(o => o.Id == id && o.Backlog.Id == backlogId);

            if (option == null)
            {
                return HttpNotFound();
            }
            this.boomContext.BacklogOptions.Remove(option);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
