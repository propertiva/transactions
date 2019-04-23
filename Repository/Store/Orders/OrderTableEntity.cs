using DataContracts.Order;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Orders
{
    public class OrderTableEntity : TableEntity
    {
        public OrderTableEntity(Order order)
        {
            this.PartitionKey = order.PropertyID;
            this.RowKey = order.OrderID;
            this.UserID = order.UserID;
            this.ChangedDateTime = order.ChangedDateTime;
            this.OrderType = order.Type;
        }

        public OrderTableEntity()
        {
        }

        public DateTime ChangedDateTime { get; set; }

        public string UserID { get; set; }

        public OrderType OrderType { get; set; }

        public bool isFilled { get; set; }

        public Order ToOrder()
        {
            var order = new Order
            {
                UserID = this.UserID,
                Type = this.OrderType,
                ChangedDateTime = this.ChangedDateTime,
                OrderID = this.RowKey,
                PropertyID = this.PartitionKey,
            };

            return order;
        }
    }
}
