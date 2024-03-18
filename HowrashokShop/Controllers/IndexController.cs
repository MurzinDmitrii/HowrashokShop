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

        // GET: Index/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Id");
            return View();
        }

        // POST: Index/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Name,Description,ThemeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Id", product.ThemeId);
            return View(product);
        }

        // GET: Index/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Id", product.ThemeId);
            return View(product);
        }

        // POST: Index/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Name,Description,ThemeId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Id", product.ThemeId);
            return View(product);
        }

        // GET: Index/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Index/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'HowrashokShopContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult OpenDocument()
        {
            string filePath = "Policy.docx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "Policy.docx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        public IActionResult AddBusket(int id)
        {
            var cookieValue = HttpContext.Request.Cookies["userlogin"];
            int clientId = _context.Clients.FirstOrDefault(c => c.Email == cookieValue).Id;
            _context.Buskets.Add(new Busket() { ProductId = id, ClientId = id});
            return Redirect("~/Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
