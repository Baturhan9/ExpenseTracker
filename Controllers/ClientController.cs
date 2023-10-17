using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly AppExpenseTrackerContext _context; 
        private readonly IHttpContextAccessor _http;
        public ClientController(ILogger<ClientController> logger, AppExpenseTrackerContext context, IHttpContextAccessor http)
        {
            _logger = logger;
            _context = context;
            _http = http;
        }

        public IActionResult Index(Client client)
        {
            _http.HttpContext.Session.Set<Client>("Client", client);

            ViewData["Name"] = _http.HttpContext.Session.Get<Client>("Client").CName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}