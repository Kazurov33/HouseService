using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseService.Models
{
   public enum ContactType
    {
        Resident = 1
    }
    public enum RequestStatuses
    {
        Created = 1,
        InWork,
        Closed
    }
}
