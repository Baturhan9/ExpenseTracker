using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces.ClientInterface
{
    public interface IClientFinder 
    {
        Client? FindClient(string login, string password); 
        Client? FindClient(string login);
    }
}