using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models;

public partial class Spending
{
    public int Id { get; set; }

    public int? ExpenseGroupId { get; set; }

    public int? ClientId { get; set; }

    public decimal? SValue { get; set; }

    public string? SExtraInfo { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ExpenseGroup? ExpenseGroup { get; set; }
}
