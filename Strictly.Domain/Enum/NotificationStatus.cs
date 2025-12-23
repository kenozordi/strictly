using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Enum
{
    public enum NotificationStatus
    {
        Pending,
        Queued,
        Completed,
        Failed,
        Expired,
        Unprocessible,
    }
}
