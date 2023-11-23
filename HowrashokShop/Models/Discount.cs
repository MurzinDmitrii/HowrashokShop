using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Discount
{
    public int Id { get; set; }

    public DateTime DateOfSetting { get; set; }

    public int ProductId { get; set; }

    public int Size { get; set; }

    public int During { get; set; }

    public virtual Product Product { get; set; } = null!;
}
