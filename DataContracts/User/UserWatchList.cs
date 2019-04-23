using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts.User
{
    public class UserWatchList
    {
        public string UserID { get; set; }

        public List<string> PropertyIDs { get; set; }
    }
}
