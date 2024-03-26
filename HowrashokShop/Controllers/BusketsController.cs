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

        // GET: Buskets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Buskets == null)
            {
                return NotFound();
            }

            var busket = await _context.Buskets
                .Include(b => b.Client)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busket == null)
            {
                return NotFound();
            }

            return View(busket);
        }

        // GET: Buskets/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Buskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ProductId,Quantity")] Busket busket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", busket.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", busket.ProductId);
            return View(busket);
        }

        // GET: Buskets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Buskets == null)
            {
                return NotFound();
            }

            var busket = await _context.Buskets.FindAsync(id);
            if (busket == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", busket.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", busket.ProductId);
            return View(busket);
        }

        // POST: Buskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ProductId,Quantity")] Busket busket)
        {
            if (id != busket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusketExists(busket.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", busket.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", busket.ProductId);
            return View(busket);
        }

        // POST: Buskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int user)
        {
            using(var context = new HowrashokShopContext())
            {
                var basket = await context.Buskets.FirstOrDefaultAsync(c => c.Id == id);
                if (basket != null)
                {
                    context.Buskets.Remove(basket);
                    await context.SaveChangesAsync();
                }
                _context = new HowrashokShopContext();
                var client = context.Clients.FirstOrDefault(c => c.Id == user);
                return RedirectToAction("Index", new { login = client.Email });
            }
        }

        private bool BusketExists(int id)
        {
          return (_context.Buskets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
