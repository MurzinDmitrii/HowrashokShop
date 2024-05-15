using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HowrashokShop.Controllers
{
    public class PayController : Controller
    {
        // GET: PayController
        public ActionResult Index(int order, double cost)
        {
            ViewData["Cost"] = cost;
            ViewData["OrderId"] = order;
            return View();
        }
    }
}
