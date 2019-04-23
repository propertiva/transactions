using DataContracts.WatchList;
using Repository.AzureStorage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.PropertyWatchList
{
    public class PropertyWatchListStore : EntityStore<WatchList, PropertyWatchListTableEntity>
    {
        private static string entityTableName = "";

        public PropertyWatchListStore() : base(entityTableName)
        {
        }

        public override PropertyWatchListTableEntity GetTableEntity(WatchList mappedEntity)
        {
            return new PropertyWatchListTableEntity(mappedEntity);
        }
    }
}
