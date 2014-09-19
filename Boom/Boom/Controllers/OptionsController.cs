using Microsoft.AspNet.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace Boom.Controllers
{
    [AccessControlAllowOrigin("*")]
    [ApplicationJsonHeader]
    public class OptionsController : BoomController
    {
        private BoomContext boomContext;

        public OptionsController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // DELETE: /options/{id}
        public IActionResult Delete(long id)
        {
            var option = this.boomContext.BacklogOptions
                .Include(o => o.Backlog)
                .SingleOrDefault(b => b.Id == id);

            this.boomContext.BacklogOptions.Remove(option);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
