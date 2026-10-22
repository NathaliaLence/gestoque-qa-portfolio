using System.Web.Mvc;

namespace Gestoque.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

      
    }
}