using DapperCRUD.Core;
using DapperCRUD.Infrastructure.Interfaz;
using FluentValidation;
using MediatR;
using System.Net;

namespace DapperCRUD.Aplication.CRUD
{
    public class GetIdStudent
    {
        public class ExeuteGetIdStudent : IRequest<StudentModel>
        {
            public int Id { get; set; }
        }
        public class ExecuteValidation : AbstractValidator<ExeuteGetIdStudent>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
            }
        }
        public class Handler : IRequestHandler<ExeuteGetIdStudent, StudentModel>
        {
            private readonly IStudents students;
            public Handler(IStudents students)
            {
                this.students = students;
            }
            public async Task<StudentModel> Handle(ExeuteGetIdStudent request, CancellationToken cancellationToken)
            {
                var result = await students.GetStudentId(request.Id) ??
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });

                return new StudentModel
                {
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    Age = result.Age,
                    Marks = result.Marks,
                    Phone = result.Phone,
                    Subjects = result.Subjects
                };
            }
        }
    }
}
