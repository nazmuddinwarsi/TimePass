using System;
using System.Collections.Generic;

namespace TimePass.DBData;

public partial class Acitvity
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? PlayNumber { get; set; }

    public DateTime? PlayTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Coins { get; set; }
}
