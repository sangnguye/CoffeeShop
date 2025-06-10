using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly CoffeeshopDbContext _context;

        public ContactController(CoffeeshopDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.CreatedAt = DateTime.Now;
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return BadRequest(ModelState);
        }
    }
}