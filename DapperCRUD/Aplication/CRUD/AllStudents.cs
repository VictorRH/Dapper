using DapperCRUD.Core;
using DapperCRUD.Infrastructure.Interfaz;
using MediatR;
using System.Net;

namespace DapperCRUD.Aplication.CRUD
{
    public class AllStudents
    {
        public class ExecuteAllStudents : IRequest<List<StudentModel>> { }
        public class Handler : IRequestHandler<ExecuteAllStudents, List<StudentModel>>
        {
            private readonly IStudents students;
            public Handler(IStudents students)
            {
                this.students = students;
            }
            public async Task<List<StudentModel>> Handle(ExecuteAllStudents request, CancellationToken cancellationToken)
            {
                var result = await students.GetAllStudents() ??
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: students not found" });

                return result.ToList();
            }
        }
    }
}
