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

        // GET: PayController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PayController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PayController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
