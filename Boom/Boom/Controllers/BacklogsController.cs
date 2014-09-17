using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Net;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Boom.Controllers
{
    public class BacklogsController : Controller
    {
        // GET: /backlogs/
        public IActionResult Get()
        {
            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = 1234;
            return this.Json(new[] { backlog });
        }

        // GET: /backlogs/{id}
        public IActionResult Get(long id)
        {
            var backlog = new Backlog();
            backlog.Name = "TestBacklog";
            backlog.Id = id;
            return this.Json(backlog);
        }

        // PUT: /backlogs/{id}
        public IActionResult Put(long id, [FromBody] string backlog)
        {
            // Backlog update = JsonConvert.DeserializeObject<Backlog>(backlog);
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }

        // DELETE: /backlogs/{id}
        public IActionResult Delete(long id)
        {
            return new HttpStatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
