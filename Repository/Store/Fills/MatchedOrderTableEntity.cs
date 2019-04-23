using DataContracts.Order;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Fills
{
    public class MatchedOrderTableEntity : TableEntity
    {
        public MatchedOrderTableEntity(MatchedOrder matchedOrder)
        {
            PartitionKey = matchedOrder.PropertyID;
            RowKey = matchedOrder.FillID;
        }

        public string OrderID { get; set; }

        public OrderType OrderType { get; set; }
    }
}
