using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HowrashokShop.Controllers
{
    public class AuthController : Controller
    {
        // GET: AuthController
        public ActionResult Index()
        {
            return View(); 
        }
    }
}
