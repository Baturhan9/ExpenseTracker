using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models;

public partial class Client
{
    public int Id { get; set; }

    public string CLogin { get; set; } = null!;

    public string CPassword { get; set; } = null!;

    public string CName { get; set; } = null!;

    public virtual ICollection<ExpenseGroup> ExpenseGroups { get; set; } = new List<ExpenseGroup>();

    public virtual ICollection<Spending> Spendings { get; set; } = new List<Spending>();
}
