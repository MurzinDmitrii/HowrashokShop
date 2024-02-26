using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int ProductId { get; set; }

    public string Comment1 { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
