using DataContracts.Order;
using Repository.AzureStorage.PersistentQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Orders
{
    /// <summary>
    /// Abstraction to interact with the Order store.
    /// </summary>
    public class OrderStore : EntityStore<Order, OrderTableEntity, OrderQueueEntity>
    {
        private static string entityTableName = "placedorders";

        private static string entityQueueName = "placedorders";

        public OrderStore() : base(entityTableName, entityQueueName)
        {
        }

        public override OrderTableEntity GetTableEntity(Order order)
        {
            return new OrderTableEntity(order);
        }

        public override OrderQueueEntity GetQueueEntity(Order order)
        {
            return new OrderQueueEntity(order);
        }

        public override string GetQueueEntityAsString(Order entity)
        {
            return GetQueueEntity(entity).ToString();
        }

        public override OrderQueueEntity GetQueueEntityFromString(string value)
        {
            return OrderQueueEntity.ConvertFromString(value);
        }
    }
}
