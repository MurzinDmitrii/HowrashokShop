using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Message
{
    public int Id { get; set; }

    public string MesText { get; set; } = null!;

    public bool Who { get; set; }

    public int ChatId { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}
