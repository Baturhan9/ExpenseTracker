using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.ExpenseGroupInterface;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositorys
{
    public class ExpenseRepository : IExpenseGroupRepository
    {
        private AppExpenseTrackerContext _db;
        public ExpenseRepository()
        {
            _db = new AppExpenseTrackerContext();
        }
        public void Create(ExpenseGroup item)
        {
            _db.ExpenseGroups.Add(item);
        }

        public void Delete(int id)
        {
            var group = _db.ExpenseGroups.Find(id);
            if(group != null)
                _db.ExpenseGroups.Remove(group);
        }

        private bool disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public List<ExpenseGroup>? GetAll()
        {
            return _db.ExpenseGroups.ToList();
        }

        public ExpenseGroup? GetOne(int id)
        {
            return _db.ExpenseGroups.Find(id);
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ExpenseGroup item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public List<ExpenseGroup>? GetExpenseGroupList(int clientId)
        {
            return _db.ExpenseGroups.Where(e => e.ClientId == clientId).ToList();
        }

        public ExpenseGroup? GetExpenseGroup(int id, int clientId)
        {
            return _db.ExpenseGroups.FirstOrDefault(e => e.ClientId==clientId && e.Id==id);
        }
    }
}