using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IQueueEntity
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }
    }
}
