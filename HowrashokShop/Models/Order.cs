using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime DateOrder { get; set; }

    public int ClientId { get; set; }

    public bool Completed { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<TablePart> TableParts { get; set; } = new List<TablePart>();
}
