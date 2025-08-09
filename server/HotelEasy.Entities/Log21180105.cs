using System;
using System.Collections.Generic;

namespace HotelEasy.Entities;

public partial class Log21180105
{
    public int LogId { get; set; }

    public string? TableName { get; set; }

    public string? OperationType { get; set; }

    public DateTime? OperationDateTime { get; set; }
}
