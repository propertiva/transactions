using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts.Order
{
    public class Order
    {
        public string OrderID { get; set; }

        public string PropertyID { get; set; }

        public DateTime ChangedDateTime { get; set; }

        public string UserID { get; set; }

        public OrderType Type { get; set; }
    }
}
