using DataContracts.Order;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Orders
{
    public class OrderQueueEntity : IQueueEntity
    {
        public OrderQueueEntity(Order order)
        {
            RowKey = order.OrderID;
            PartitionKey = order.PropertyID;
        }

        public string RowKey { get; set; }

        public string PartitionKey { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static OrderQueueEntity ConvertFromString(string value)
        {
            var entity = JsonConvert.DeserializeObject<OrderQueueEntity>(value);
            return entity;
        }
    }
}
