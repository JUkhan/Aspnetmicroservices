using System;
using System.Threading;
using System.Threading.Tasks;
using Discount.Grpcn;
using Grpc.Core;
using Grpc.Net.Client;


namespace ConApp
{
    class Program
    {
        static async Task  Main(string[] args)
        {
           
            var channel =GrpcChannel.ForAddress("https://localhost:5001/");
            var service = new Greeter.GreeterClient(channel);
            var res = await service.SayHelloAsync(new HelloRequest { Name = "Jasim" });
            Console.WriteLine(res.Message);
            Console.ReadKey();
        }
    }
    
}
