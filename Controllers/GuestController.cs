using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Controllers
{
    public class GuestController : Controller
    {
        private readonly ILogger<GuestController> _logger;
        private readonly AppExpenseTrackerContext _context;

        public GuestController(ILogger<GuestController> logger, AppExpenseTrackerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string clientLogin, string clientPassword)
        {
            if(clientLogin == "" || clientPassword == "")
            {
                ViewBag.EmptyFields = "Fill in all the data";
                return View();
            }
            var client = _context.Clients.FirstOrDefault(c => c.CLogin == clientLogin && c.CPassword == clientPassword);
            if(client == null)
            {
                ViewBag.NotFound = "Not right password or login";
                return View();
            }
            return RedirectToAction("Index","Client", client);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}