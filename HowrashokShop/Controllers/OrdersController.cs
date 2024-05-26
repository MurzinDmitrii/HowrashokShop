using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HowrashokShop.Models;
using Microsoft.Extensions.Hosting;

namespace HowrashokShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HowrashokShopContext _context;

        public OrdersController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string email)
        {
            var howrashokShopContext = _context.Orders;
            var client = _context.Clients.FirstOrDefault(c => c.Email== email);
            var list = howrashokShopContext.ToList();
            list = list.Where(c => c.ClientId == client.Id).ToList();
            foreach(var item in list)
            {
                item.Status = _context.Statuses.FirstOrDefault(c => c.Id == item.StatusId);
            }
            return View(list);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(c => c.TableParts)
                .FirstOrDefaultAsync(m => m.Id == id);
            foreach(var item in order.TableParts)
            {
                var product = _context.Products.FirstOrDefault(c => c.Id == item.ProductId);
                product.Photos = _context.Photos.Where(c => c.ProductId == item.ProductId).ToList();
                product.Costs = _context.Costs.Where(c => c.ProductId == item.ProductId).ToList();
                product.Category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                item.Product = product;
            }
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(string login)
        {
            ViewData["DateOrder"] = DateTime.Now;
            Client client = _context.Clients.FirstOrDefault(c => c.Email == login);
            ViewData["Client"] = client.Id;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOrder,ClientId,Completed,Address")] Order order)
        {
            order.DateOrder = DateTime.Now;
            order.Client = _context.Clients.FirstOrDefault(c => c.Id == order.ClientId);
            order.Status = _context.Statuses.FirstOrDefault(c => c.Id == 1);
            if (order.Address == null) order.Address = "";
            ModelState.ClearValidationState(nameof(Order));
            _context.Add(order);
            await _context.SaveChangesAsync();
            double cost = 0.0;
            var list = _context.Buskets.Where(c => c.Client == order.Client).ToList();
            foreach (var item in list)
            {
                var product = _context.Products.FirstOrDefault(c => c.Id == item.ProductId);
                product.Arhived = true;
                cost += Convert.ToDouble(item.Product.Cost);
                TablePart part = new TablePart();
                part.ProductId = item.ProductId;
                part.Quantity = 1;
                part.OrderId = order.Id;
                part.DateOrder = order.DateOrder;
                using(HowrashokShopContext context = new())
                {
                    context.Products.Update(product);
                    context.TableParts.Add(part);
                    context.Buskets.Remove(item);
                    await context.SaveChangesAsync();
                }
            }

            return Redirect("~/Pay?order="+order.Id+"&cost="+cost);
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
