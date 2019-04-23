using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AzureStorage.PersistentQueue
{
    public abstract class EntityStore<T, TE, TQE> : IEntityStore<T>
        where T: class
        where TE: TableEntity
        where TQE: IQueueEntity
    {
        private string entityTable;

        private string entityQueue;

        public EntityStore(string entityTableName, string entityQueueName)
        {
            entityQueue = entityQueueName;
            entityTable = entityTableName;
        }

        protected static readonly string StorageName = "";
          
        protected static readonly string StorageKey = "";          

        public abstract TE GetTableEntity(T entity);

        public abstract TQE GetQueueEntity(T entity);

        public abstract string GetQueueEntityAsString(T entity);

        public abstract TQE GetQueueEntityFromString(string value);

        public async Task<bool> Put(T entity)
        {
            var stored = false;

            try
            {
                var storedEntity = GetTableEntity(entity);

                // try add the order to the order log
                var tableClient = GetCloudTableClient();
                CloudTable orderTable = tableClient.GetTableReference(entityTable);

                await orderTable.CreateIfNotExistsAsync();
                TableOperation upsertOperation = TableOperation.InsertOrMerge(storedEntity);

                var result = await orderTable.ExecuteAsync(upsertOperation);

                // try add the order to the order queue
                CloudQueueClient queueClient = GetCloudQueueClient();
                    
                // Retrieve a reference to a container.
                CloudQueue queue = queueClient.GetQueueReference(entityQueue);

                // Create the queue if it doesn't already exist
                await queue.CreateIfNotExistsAsync();

                var payload = GetQueueEntityAsString(entity);
                CloudQueueMessage message = new CloudQueueMessage(payload);
                await queue.AddMessageAsync(message);

                stored = true;
            }
            catch (Exception e)
            {
                stored = false;
            }

            return stored;
        }

        private CloudQueueClient GetCloudQueueClient()
        {
            // Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var storageCredentials = new StorageCredentials(StorageName, StorageKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                 storageCredentials,
                 true);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            return queueClient;
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

        public async Task<T> Get()
        {
            var queueClient = GetCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(entityQueue);
            CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();

            var queueEntity = GetQueueEntityFromString(retrievedMessage.AsString);
            TableOperation retrieveOperation = TableOperation.Retrieve<TE>(queueEntity.PartitionKey, queueEntity.RowKey);
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

        public async Task Delete(T contract)
        {
            var queueClient = GetCloudQueueClient();
            var queue = queueClient.GetQueueReference(entityQueue);

            var message = new CloudQueueMessage(GetQueueEntityAsString(contract));
            await queue.DeleteMessageAsync(message);
        }
    }
}
