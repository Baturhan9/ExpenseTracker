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
<<<<<<< HEAD
=======
using ExpenseTracker.Models.ViewModels.Client;
>>>>>>> ExpenseController
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
<<<<<<< HEAD
        private readonly IHttpContextAccessor _http;
        public ClientController(ILogger<ClientController> logger,IHttpContextAccessor http)
        {
            _logger = logger;
            _http = http;
            _repository = new ClientRepository();
=======
        public ClientController(ILogger<ClientController> logger, IRepository<Client> repository)
        {
            _logger = logger;
            _repository = repository;
>>>>>>> ExpenseController
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
<<<<<<< HEAD
            return RedirectToAction("ClientMenu");
        }
        public IActionResult ClientMenu() => View();

=======

            HttpContext.Session.SetInt32("ClientId", client.Id);
            return RedirectToAction("ClientMenu");
        }
        public IActionResult ClientMenu()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if(clientId == null)
                return NotFound();
            var client = _repository.GetOne(clientId.Value);
            ViewData["ClientName"] = client.CName;
            return View();
        }
        public IActionResult CreateClient() => View();
        [HttpPost]
        public IActionResult CreateClient(CreateClientViewModel clientObj)
        {
            if(!ModelState.IsValid)
            {
                return View(clientObj);
            }

            if(_finder.FindClient(clientObj.Login) != null) 
            {
                ViewBag.Exist = "this login already exists, make up a new one";
                return View(clientObj);
            }
            
            var client = new Client()
            {
                CName = clientObj.Name,
                CLogin = clientObj.Login,
                CPassword = clientObj.Password
            };
            _repository.Create(client);
            _repository.Save();
            
            return RedirectToAction("Index");
        }
>>>>>>> ExpenseController
        
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