using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Ids { get; set; } = new List<Product>();
}
