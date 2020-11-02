using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;


namespace console_app
{
    class Program
    {
       public static int x=0;
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        private static CloudTable testamTable;

        static void Main(string[] args)
        {
            Task.Run(async()=>{ await Initialize(); })
            .GetAwaiter()
            .GetResult();

            Task.Run(async()=>{ await Initialize2(); })
            .GetAwaiter()
            .GetResult();
        }

        static async Task Initialize()
        {
            
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=datclungraul;AccountKey=KHXut9wT1usu/+1OtTcVUu0UF/Jjp+pvMQquqxxHmhi9Bsx8Fiwi4+PkutsmmXLIdS02Iu5q8fgNVlKu++KnfA==;EndpointSuffix=core.windows.net";
              var account=CloudStorageAccount.Parse(storageConnectionString);
             tableClient= account.CreateCloudTableClient();
              studentsTable=tableClient.GetTableReference("studenti");
               await studentsTable.CreateIfNotExistsAsync();
               await GetAllStudents();
        }
        static async Task Initialize2()
        {
            
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=datclungraul;AccountKey=KHXut9wT1usu/+1OtTcVUu0UF/Jjp+pvMQquqxxHmhi9Bsx8Fiwi4+PkutsmmXLIdS02Iu5q8fgNVlKu++KnfA==;EndpointSuffix=core.windows.net";
              var account=CloudStorageAccount.Parse(storageConnectionString);
             tableClient= account.CreateCloudTableClient();
              matricaTable=tableClient.GetTableReference("matrici");
               await matricaTable.CreateIfNotExistsAsync();
               await AddNewMatrica();
        }
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universiate\t CNP\tEmail\t PhoneNumber\tAn \t Nume\t Prenume ");
            TableQuery<StudentEntity> query= new TableQuery<StudentEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "UPT"));
            TableContinuationToken token=null;
            
            do{
                TableQuerySegment<StudentEntity> resultSegment= await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token= resultSegment.ContinuationToken;
                foreach(StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey, entity.RowKey, entity.Nume, entity.Prenume, entity.Email,entity.PhoneNumber,entity.An,entity.Facultate);
                    x++;
                    

                }
                Console.WriteLine(x);

            }while(token!=null);
        }
       
        
        private static async Task AddNewStudent()
        {
            var student =new StudentEntity("UPT","123456789");
            
            student.Prenume="Iacob";
            student.Nume="Vasila";
           
            student.Email="vasilica@gmail.com";
           student.An=1;
            student.PhoneNumber="0740777111";
            student.Facultate="AC";

            var insertOperation=TableOperation.Insert(student);
            await studentsTable.ExecuteAsync(insertOperation);


        }
         private static async Task AddNewMatrica()
        {
            
            var test =new Testam("UPT"," " );
            
            matrica.RowKey = DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss");
            matrica.Count=x;
           

            var insertOperation=TableOperation.Insert(matrica);
            await testTable.ExecuteAsync(insertOperation);


        }
        private static async Task EditStudent()
        {
            var student= new StudentEntity("UPT","098776543");
            student.Prenume="Irinel";
           student.An=4;
           
            student.ETag="*";
            var editOperation=TableOperation.Merge(student);

            await studentsTable.ExecuteAsync(editOperation);
        }
    }
}
