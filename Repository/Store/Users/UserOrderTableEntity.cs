using DataContracts.User;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Users
{
    public class UserOrderTableEntity : TableEntity
    {
        public UserOrderTableEntity(UserOrder userOrder)
        {
            PartitionKey = userOrder.UserID;
            RowKey = userOrder.PropertyID;
        }

        public string[] OrderFillIDs { get; set; }
    }
}
