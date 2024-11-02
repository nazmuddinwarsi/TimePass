using System;
using System.Collections.Generic;

namespace TimePass.DBData;

public partial class RptReward
{
    public int? Userid { get; set; }

    public string? Playnumber { get; set; }

    public DateTime? Playtime { get; set; }

    public int? Coin { get; set; }
}
