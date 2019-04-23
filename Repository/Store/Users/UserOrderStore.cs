using DataContracts.User;
using Repository.AzureStorage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Store.Users
{
    public class UserOrderStore : EntityStore<UserOrder, UserOrderTableEntity>
    {
        private static string entityTableName = "";

        public UserOrderStore() : base(entityTableName)
        {
        }

        public override UserOrderTableEntity GetTableEntity(UserOrder mappedEntity)
        {
            return new UserOrderTableEntity(mappedEntity);
        }
    }
}
