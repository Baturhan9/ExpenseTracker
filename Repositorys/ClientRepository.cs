using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.ClientInterface;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositorys;

public class ClientRepository : IRepository<Client>, IClientFinder
{
    private AppExpenseTrackerContext db;
    public ClientRepository()
    {
        db = new AppExpenseTrackerContext();
    }
    public void Create(Client item)
    {
        db.Clients.Add(item);
    }

    public void Delete(int id)
    {
        var client = db.Clients.Find(id);
        if(client != null)
            db.Clients.Remove(client);
    }

    private bool disposed = false;
 
    public virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }

    public List<Client>? GetAll()
    {
        return db.Clients.ToList();
    }

    public Client? GetOne(int id)
    {
        return db.Clients.Find(id);
    }

    public void Save()
    {
        db.SaveChanges();
    }

    public void Update(Client item)
    {
        db.Entry(item).State = EntityState.Modified;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Client? FindClient(string loing, string password)
    {
        return db.Clients.FirstOrDefault(c => c.CLogin == loing && c.CPassword == password);
    }
<<<<<<< HEAD
=======
    public Client? FindClient(string login)
    {
        return db.Clients.FirstOrDefault(c=> c.CLogin == login);
    }
>>>>>>> ExpenseController
}