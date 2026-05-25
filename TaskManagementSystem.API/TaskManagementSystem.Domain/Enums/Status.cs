using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Domain.Enums
{
    public enum TaskItemStatus
    {
       Open = 1,
       InProgress = 2,
       Investigation = 3,
        Resolved = 4,
        Escalated = 5,
        WaitingForCustomer = 6,
        Cancelled = 7,
        Closed = 8
    }
}
