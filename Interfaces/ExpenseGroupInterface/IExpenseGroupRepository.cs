using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces.ExpenseGroupInterface
{
    public interface IExpenseGroupRepository : IRepository<ExpenseGroup>
    {
        List<ExpenseGroup>? GetExpenseGroupList(int clientId);
        ExpenseGroup? GetExpenseGroup(int id, int clientId);
    }
}