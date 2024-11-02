using System;
using System.Collections.Generic;

namespace TimePass.DBData;

public partial class Result
{
    public int Id { get; set; }

    public DateTime PlayTime { get; set; }

    public string? PlayNumber { get; set; }

    public int? TotalWinner { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Active { get; set; }
}
