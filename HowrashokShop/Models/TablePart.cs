using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class TablePart
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public DateTime DateOrder { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
