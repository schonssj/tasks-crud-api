using System.Web.Mvc;

namespace TasksControl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Tasks Control";

            return View();
        }
    }
}
