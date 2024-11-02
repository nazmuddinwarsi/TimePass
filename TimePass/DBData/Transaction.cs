using System;
using System.Collections.Generic;

namespace TimePass.DBData;

public partial class Transaction
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Coins { get; set; }

    public DateTime? TTime { get; set; }
}
