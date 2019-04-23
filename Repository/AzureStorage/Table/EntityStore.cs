using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Repository.Interfaces.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AzureStorage.Table
{
    public abstract class EntityStore<T, TE> : IEntityStore<T>
        where T : class
        where TE : TableEntity
    {
        private readonly string entityTable;

        public EntityStore(string entityTableName)
        {
            entityTable = entityTableName;
        }

        protected static readonly string StorageName = "";

        protected static readonly string StorageKey = "";

        public async Task<T> Get(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<TE>(partitionKey, rowKey);
            var tableClient = GetCloudTableClient();
            var table = tableClient.GetTableReference(entityTable);
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            if (retrievedResult.Result == null)
            {
                return null;
            }

            T resultObj = retrievedResult as T;
            return resultObj;
        }

        private CloudTableClient GetCloudTableClient()
        {
            // Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var storageCredentials = new StorageCredentials(StorageName, StorageKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                 storageCredentials,
                 true);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            return tableClient;
        }

        public async Task<bool> Put(T mappedEntity)
        {
            var storedEntity = GetTableEntity(mappedEntity);

            // try add the order to the order log
            var tableClient = GetCloudTableClient();
            CloudTable orderTable = tableClient.GetTableReference(entityTable);

            await orderTable.CreateIfNotExistsAsync();
            TableOperation upsertOperation = TableOperation.InsertOrMerge(storedEntity);

            var result = await orderTable.ExecuteAsync(upsertOperation);
            return result != null;
        }

        public abstract TE GetTableEntity(T mappedEntity);
    }
}
