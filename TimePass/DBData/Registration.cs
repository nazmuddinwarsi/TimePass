using System;
using System.Collections.Generic;

namespace TimePass.DBData;

public partial class Registration
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }

    public string? Photo { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Coins { get; set; }
}
