﻿using System;
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
    public class CommentsController : Controller
    {
        private readonly HowrashokShopContext _context;

        public CommentsController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var howrashokShopContext = _context.Comments.Include(c => c.Client).Include(c => c.Product);
            return View(await howrashokShopContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Client)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create(int id)
        {
            var tablePart = _context.TableParts.FirstOrDefault(m => m.Id == id);
            tablePart.Product = _context.Products.FirstOrDefault(c => c.Id == tablePart.ProductId);
            tablePart.Order = _context.Orders.FirstOrDefault(c => c.Id == tablePart.OrderId);
            Comment comment = _context.Comments.FirstOrDefault(c => c.ProductId == tablePart.ProductId);
            var client = _context.Clients.FirstOrDefault(c => c.Id == tablePart.Order.ClientId);
            if (tablePart.Order.StatusId != 4) return Redirect("~/Orders?email=" + client.Email);//тут исправить потом
            
            if (comment != null)
            {
                client = _context.Clients.FirstOrDefault(c => c.Id == comment.ClientId);
                return Redirect("~/Orders?email=" + client.Email);
            }

            tablePart.Order = _context.Orders.FirstOrDefault(c => c.Id == tablePart.OrderId);
            //comment.Client = _context.Clients.FirstOrDefault(c => c.Id == tablePart.Order.ClientId);
            ViewData["Product"] = tablePart.ProductId;
            return View();
        }

        public async Task<IActionResult> Created(string user, int id, string comment1, int mark)
        {
            Comment comment = new();
            comment.Product = _context.Products.FirstOrDefault(c => c.Id == id);
            comment.Client = _context.Clients.FirstOrDefault(c => c.Email == user);
            comment.ClientId = comment.Client.Id;
            comment.ProductId = comment.Product.Id;
            comment.Comment1 = comment1 ?? "";
            comment.Mark = mark;
            _context.Add(comment);
            await _context.SaveChangesAsync();
            var client = _context.Clients.FirstOrDefault(c => c.Id == comment.ClientId);
            return Redirect("~/Orders/Index?email=" + client.Email);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string user, int id, string comment1, int mark)
        {
            Comment comment = new();
            comment.Product = _context.Products.FirstOrDefault(c => c.Id == id);
            comment.Client = _context.Clients.FirstOrDefault(c => c.Email == user);
            comment.Comment1 = comment1??"";
            comment.Mark = mark;
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return Redirect("~/Orders/Index?email" + comment.Client.Email);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", comment.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", comment.ProductId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ProductId,Comment1")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", comment.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", comment.ProductId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Client)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'HowrashokShopContext.Comments'  is null.");
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
          return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
