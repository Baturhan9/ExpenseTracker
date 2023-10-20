using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        List<T>? GetAll();
        T? GetOne(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}