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
    public class ArhiveController : Controller
    {
        private readonly HowrashokShopContext _context;

        public ArhiveController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Arhive
        public async Task<IActionResult> Index(string searchString)
        {
            int index = 0;

            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                product.Category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                product.Theme = _context.Themes.FirstOrDefault(t => t.Id == product.ThemeId);
                product.Costs = _context.Costs.Where(cost => cost.ProductId == product.Id).ToList();
                product.Photos = _context.Photos.Where(photo => photo.ProductId == product.Id).ToList();
            }
            var productsList = products;

            if (!String.IsNullOrEmpty(searchString))
            {
                productsList = productsList.Where(s => s.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(productsList);
        }

        // GET: Arhive/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
