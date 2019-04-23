using DataContracts.Order;
using Repository.AzureStorage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Fills
{
    public class MatchedOrderStore : EntityStore<MatchedOrder, MatchedOrderTableEntity>
    {
        private static string entityTableName = "";

        public MatchedOrderStore() : base(entityTableName)
        {
        }

        public override MatchedOrderTableEntity GetTableEntity(MatchedOrder mappedEntity) => new MatchedOrderTableEntity(mappedEntity);
    }
}
