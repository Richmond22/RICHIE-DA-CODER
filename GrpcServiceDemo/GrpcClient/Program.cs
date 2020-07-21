using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceDemo;

namespace GrpcClient
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Student.StudentClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(100));
            using var streamingCall = client.getStudentsAsync(new Empty(), cancellationToken: cts.Token);
            {
                await foreach (var student in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"id : {student.Id} age is {student.Age}");
                }
            }

            var grades = client.getStudentGrades(new StudentRequest { Id = 2 });

            Console.ReadLine();
        }

    }
}
