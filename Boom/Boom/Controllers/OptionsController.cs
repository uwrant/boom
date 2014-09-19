using Microsoft.AspNet.Mvc;
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
            var option = this.boomContext.BacklogOptions.SingleOrDefault(b => b.Id == id);
            this.boomContext.Delete(option);
            this.boomContext.SaveChanges();
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
