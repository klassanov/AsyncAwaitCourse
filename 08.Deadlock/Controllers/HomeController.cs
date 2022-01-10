using System.Threading.Tasks;
using System.Web.Mvc;

namespace _08.Deadlock.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> AsyncAction()
        {
            await WaitASecond();
            return Content("Hello from the Async Action");
        }

        public ActionResult SyncAction()
        {
            WaitASecond().Wait();
            return Content("Hello from the Sync Action");
        }

        private async Task WaitASecond()
        {
            await Task.Delay(1000);//.ConfigureAwait(false); to fix: continuation on another thread that is not blocked
        }
    }
}