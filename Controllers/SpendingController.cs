using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Interfaces.SpendingInterface;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Controllers
{
    public class SpendingController : Controller
    {
        private readonly ILogger<SpendingController> _logger;
        private readonly ISpendingRepository _repository;

        public SpendingController(ILogger<SpendingController> logger, ISpendingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddSpending(int clientId, decimal valueSpend, int groupId)
        {
             var spend = new Spending()
             {
                ClientId = clientId,
                ExpenseGroupId = groupId,
                SValue = valueSpend
             };
             _repository.Create(spend);
             _repository.Save();
            return RedirectToAction("ListExpense","Expense");
        }
        public IActionResult ListSpending()
        {
            int? clientId = HttpContext.Session.GetInt32("ClientId");
            if(clientId == null)
                return NotFound();
            var listSpending = _repository.GetByClient(clientId.Value);
            return View(listSpending);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}