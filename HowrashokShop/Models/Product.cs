using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ThemeId { get; set; }

    public bool? Arhived { get; set; }

    public virtual ICollection<Busket> Buskets { get; set; } = new List<Busket>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Cost> Costs { get; set; } = new List<Cost>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<TablePart> TableParts { get; set; } = new List<TablePart>();

    public virtual Theme Theme { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
