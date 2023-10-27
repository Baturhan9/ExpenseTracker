using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.ExpenseGroupInterface;
using ExpenseTracker.Interfaces.SpendingInterface;
using ExpenseTracker.Models;
using ExpenseTracker.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseGroupRepository _repository;
        private readonly ISpendingRepository _spendingRep;

        public ExpenseController(ILogger<ExpenseController> logger, IExpenseGroupRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _spendingRep = new SpendingRepository();
        }

        public IActionResult ListExpense()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if(clientId == null)
                return NotFound();
            var listGroup = _repository.GetExpenseGroupList(clientId.Value);
            if(listGroup != null)
            foreach(var item in listGroup)
                CalculateTotalCost(item);
            ViewData["ClientId"]= clientId;
            return View(listGroup);
        } 

        [HttpPost]
        public IActionResult CreateGroup(int clientId, string nameGroup, string descriptionGroup)
        {
            ExpenseGroup expenseGroup = new ExpenseGroup()
            {
                ClientId = clientId,
                EgName = nameGroup,
                EgExtraInfo = descriptionGroup,
                EgAmountSpends = 0
            };
            _repository.Create(expenseGroup);
            _repository.Save();
            return RedirectToAction("ListExpense");
        }

        private void CalculateTotalCost(ExpenseGroup group)
        {
            group.EgAmountSpends = 0;
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if(clientId == null)
                return;
            var listSpend = _spendingRep.GetByClientAndGroup(clientId.Value,group.Id);
            for(int i = 0; i < listSpend.Count; i++)
                group.EgAmountSpends += listSpend[i].SValue;
           _repository.Update(group);
           _repository.Save(); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}