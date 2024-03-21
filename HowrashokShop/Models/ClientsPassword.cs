using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HowrashokShop.Models;

public partial class ClientsPassword
{
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
