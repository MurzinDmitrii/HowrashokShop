using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Cost
{
    public int ProductId { get; set; }

    public DateTime DateOfSetting { get; set; }

    public decimal Size { get; set; }

    public virtual Product Product { get; set; } = null!;
}
