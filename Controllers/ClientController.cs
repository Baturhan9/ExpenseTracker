using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Data;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.ClientInterface;
using ExpenseTracker.Models;
using ExpenseTracker.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IRepository<Client> _repository;
        private readonly IClientFinder _finder;
        private readonly IHttpContextAccessor _http;
        public ClientController(ILogger<ClientController> logger,IHttpContextAccessor http)
        {
            _logger = logger;
            _http = http;
            _repository = new ClientRepository();
            _finder = new ClientRepository();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Index(string clientLogin, string clientPassword)
        {
            if(isEmptyField(clientLogin, clientPassword))
            {
                ViewBag.EmptyFields = "missing fields";
                return View();
            }
            var client = _finder.FindClient(clientLogin, clientPassword);
            if(client == null)
            {
                ViewBag.NotFound = "no user was found";
                return View();
            }
            return RedirectToAction("ClientMenu");
        }
        public IActionResult ClientMenu() => View();

        
        private bool isEmptyField(string s1, string s2)
        {
            if(s1 is null || s2 is null)
                return true;
           return false; 
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}