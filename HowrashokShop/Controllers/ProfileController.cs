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
    public class ProfileController : Controller
    {
        private readonly HowrashokShopContext _context;

        public ProfileController(HowrashokShopContext context)
        {
            _context = context;
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,Email,Birthday,ClientsPassword.Password")] Client client, ClientsPassword clientsPassword)
        {
            clientsPassword.Client = client;
            clientsPassword.ClientId = client.Id;
            if(clientsPassword.Password != null)
            {
                clientsPassword.Password = Cryptography.Cryptography.HashPassword(clientsPassword.Password);
            }
            else
            {
                ModelState.AddModelError("ClientsPassword.Password", "Пароль не может быть пустым");
            }
            ModelState.ClearValidationState(nameof(ClientsPassword));

            if (!TryValidateModel(clientsPassword, nameof(ClientsPassword)))
            {
                return View(client);
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }
            if (ModelState.IsValid)
            {
                var login = _context.Clients.FirstOrDefaultAsync(c => c.Email == client.Email);
                if (await login != null)
                {
                    ModelState.AddModelError("Email", "Такой email уже зарегистрирован");
                    return View(client);
                }
                await _context.AddAsync(client);
                await _context.AddAsync(clientsPassword);
                await _context.SaveChangesAsync();
                return Redirect("~/Auth");
            }
            return View(client);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(string login)
        {
            if (login == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == login);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,Email,Birthday")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
