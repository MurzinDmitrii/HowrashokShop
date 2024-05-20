using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HowrashokShop.Models;
using System.Drawing;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using SimMetrics.Net.Metric;

namespace HowrashokShop.Controllers
{
    public class IndexController : Controller
    {
        private readonly HowrashokShopContext _context;

        public IndexController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index(string searchString)
        {
            int index = 0;

            JaroWinkler jaro = new();

            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                product.Category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                product.Theme = _context.Themes.FirstOrDefault(t => t.Id == product.ThemeId);
                product.Costs = _context.Costs.Where(cost => cost.ProductId == product.Id).ToList();
                product.Photos = _context.Photos.Where(photo => photo.ProductId == product.Id).ToList();
                product.Discounts = _context.Discounts.Where(c => c.ProductId == product.Id).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (from p in products
                                               where
                                jaro.GetSimilarity(p.Description.ToLower(), searchString.ToLower()) > 0.5
                                               orderby jaro.GetSimilarity(p.Description.ToLower(), searchString.ToLower())
                                               descending
                                               select p).ToList();
            }

            return View(products);
        }

        // GET: Index/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            product.Category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            product.Theme = _context.Themes.FirstOrDefault(t => t.Id == product.ThemeId);
            product.Costs = _context.Costs.Where(cost => cost.ProductId == product.Id).ToList();
            product.Photos = _context.Photos.Where(photo => photo.ProductId == product.Id).ToList();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult OpenDocument()
        {
            string filePath = "Policy.docx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "Policy.docx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        public IActionResult AddBusket(string user, int id)
        {
            var cookieValue = HttpContext.Request.Cookies["userlogin"];
            int clientId = _context.Clients.FirstOrDefault(c => c.Email == user).Id;
            _context.Buskets.Add(new Busket() { ProductId = id, ClientId = clientId});
            _context.SaveChanges();
            return Redirect("~/Index");
        }
    }
}
