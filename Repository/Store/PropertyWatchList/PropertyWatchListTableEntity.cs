using DataContracts.WatchList;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.PropertyWatchList
{
    public class PropertyWatchListTableEntity : TableEntity
    {
        public PropertyWatchListTableEntity(WatchList watchList)
        {
            PartitionKey = watchList.UserID;
            RowKey = watchList.PropertyID;
        }
    }
}
