using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Boom.Controllers
{
    public class BacklogController : Controller
    {
        // GET: /backlog/
        public IActionResult Index()
        {
            return this.Content("hi");
        }
    }
}
