using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Interfaces.SpendingInterface;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repositorys
{
    public class SpendingRepository : ISpendingRepository
    {
        private AppExpenseTrackerContext _db;
        public SpendingRepository()
        {
            _db = new AppExpenseTrackerContext();
        }
        public void Create(Spending item)
        {
            _db.Spendings.Add(item);
        }

        public void Delete(int id)
        {
            var spend = _db.Spendings.Find(id);
            if(spend != null)
            _db.Spendings.Remove(spend);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Spending>? GetAll()
        {
            return _db.Spendings.ToList();
        }

        public List<Spending> GetByClient(int clientId)
        {
            return _db.Spendings.Where(s => s.ClientId == clientId).ToList();
        }

        public List<Spending> GetByClientAndGroup(int clientId, int groupId)
        {
            return _db.Spendings.Where(s=>s.ClientId == clientId && s.ExpenseGroupId == groupId).ToList();
        }

        public Spending? GetOne(int id)
        {
            return _db.Spendings.Find(id);
        }

        public Spending GetSpending(int id, int clientId, int groupId)
        {
            return _db.Spendings.FirstOrDefault(s=>s.ClientId == clientId && s.ExpenseGroupId == groupId && s.Id==id) ?? new Spending(); 
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Spending item)
        {
            _db.Spendings.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}