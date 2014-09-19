using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
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
            var options = this.boomContext.BacklogOptions.Where(o => o.Backlog.Id == backlogId && o.BacklogId == backlogId).ToList();
            return this.Json(options);
        }

        // GET: /backlogs/{backlogId}/options/{id}
        public IActionResult Get(long backlogId, long id)
        {
            var option = this.boomContext.BacklogOptions.SingleOrDefault(o => o.Id == id && o.BacklogId == backlogId);
            if (option == null)
            {
                return this.HttpNotFound();
            }
            return this.Json(option);
        }

        // POST:  /backlogs/{backlogId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Post(long backlogId, [FromBody] BacklogOption option)
        {
            var backlog = boomContext.Backlogs.SingleOrDefault(b => b.Id == backlogId);
            if (backlog == null)
            {
                return HttpNotFound();
            }

            boomContext.Add(option);

            backlog.Options.Add(option);
            option.Backlog = backlog;
            option.BacklogId = backlogId;

            boomContext.SaveChanges();
            return this.Json(option);
        }

        // PUT: /backlogs/{backlogId}/options/{id}
        // BODY: {"Description":"Option Description"}
        public IActionResult Put(long backlogId, long id, [FromBody] BacklogOption option)
        {
            var persistedOption = this.boomContext.BacklogOptions.SingleOrDefault(o => o.Id == id && o.BacklogId == backlogId);
            if (persistedOption == null)
            {
                return this.HttpNotFound();
            }

            persistedOption.Description = option.Description;

            this.boomContext.SaveChanges();
            return this.Json(option);
        }

        // DELETE: /backlogs/{backlogId}/options/{id}
        public IActionResult Delete(long backlogId, long id)
        {
            var option = this.boomContext.BacklogOptions.SingleOrDefault(o => o.Id == id && o.BacklogId == backlogId);
            if (option == null)
            {
                return this.HttpNotFound();
            }
            this.boomContext.Delete(option);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
