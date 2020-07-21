using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcServiceDemo.Services
{
    public class StudentService : Student.StudentBase
    {
        public override Task<StudentResponse> getStudent(StudentRequest request, ServerCallContext context)
        {
            var studentList = new List<StudentResponse>()
            {
                new StudentResponse()
                {
                    Id = 1,
                    Name = "Richmond",
                    Surname = "Matsama",
                    Age = 18
                }
            };

            return Task.FromResult(studentList.First(student => student.Id == request.Id));
        }

        public override async Task getStudentsAsync(Empty request, IServerStreamWriter<StudentResponse> responseStream, ServerCallContext context)
        { 
            var rand = new Random();
            var numStud = 0;
            while (!context.CancellationToken.IsCancellationRequested && numStud < 50)
            {
                numStud++;
                var student = new StudentResponse
                {
                    Id = rand.Next(1, int.MaxValue),
                    Age = rand.Next(18, 30)
                };
               await Task.Delay(2000);
              await  responseStream.WriteAsync(student);
            }
        }

        public override Task<StudentGrades> getStudentGrades(StudentRequest request, ServerCallContext context)
        {
            return Task.FromResult(new StudentGrades
            {
                StudentId = 1,
                Grades = 78,
                Subject = "Mathematics"
            });
        }
    }
}
