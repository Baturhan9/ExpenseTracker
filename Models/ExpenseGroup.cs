using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models;

public partial class ExpenseGroup
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string? EgName { get; set; }

    public decimal? EgAmountSpends { get; set; }

    public string? EgExtraInfo { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Spending> Spendings { get; set; } = new List<Spending>();
}
