using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Photo
{
    public int Id { get; set; }

    public string Photopath { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
