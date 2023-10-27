using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces.SpendingInterface
{
    public interface ISpendingRepository : IRepository<Spending>
    {
        List<Spending> GetByClient(int clientId);
        List<Spending> GetByClientAndGroup(int clientId, int groupId);
        Spending GetSpending(int id, int clientId, int groupId);
    }
}