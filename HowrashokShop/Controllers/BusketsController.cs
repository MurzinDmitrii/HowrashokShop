 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HowrashokShop.Models;

namespace HowrashokShop.Controllers
{
    public class BusketsController : Controller
    {
        private HowrashokShopContext _context;

        public BusketsController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Buskets
        public async Task<IActionResult> Index(string login)
        {
            var howrashokShopContext = _context.Buskets.Include(b => b.Client).Include(b => b.Product);
            var productInBusketList = howrashokShopContext.Where(c => c.Client.Email == login).ToList();
            foreach (var item in productInBusketList)
            {
                item.Product.Category = _context.Categories.FirstOrDefault(c => c.Id == item.Product.CategoryId);
                item.Product.Costs = _context.Costs.Where(c => c.ProductId == item.Product.Id).ToList();
                item.Product.Photos = _context.Photos.Where(c => c.ProductId == item.Product.Id).ToList();
            }
            return View(productInBusketList);
        }
    }
}
