using HowrashokShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HowrashokShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnHashController : ControllerBase
    {
        private readonly HowrashokShopContext _context;
        public ReturnHashController(HowrashokShopContext context)
        {
            _context = context;
        }

        public async Task<string> Index(string str)
        {
            var hash = Cryptography.Cryptography.HashPassword(str);

            return hash;
        }
    }
}
