using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class Testam : TableEntity
    {
        

        public Testam(string universiate, string timestamp)
        {
            this.PartitionKey = universiate;
            this.RowKey = timestamp;
          
        }

        public Testam() { }

        public int Count { get; set; }
    }
}