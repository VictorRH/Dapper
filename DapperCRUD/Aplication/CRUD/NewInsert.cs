using DapperCRUD.Infrastructure.Interfaz;
using FluentValidation;
using MediatR;
using System.Net;

namespace DapperCRUD.Aplication.CRUD
{
    public class NewInsert
    {
        public class ExecuteRequestAdd : IRequest
        {
            public string Firstname { get; set; } = string.Empty;
            public string Lastname { get; set; } = string.Empty;
            public string Subjects { get; set; } = string.Empty;
            public int Age { get; set; }
            public string Phone { get; set; } = string.Empty;
            public string Marks { get; set; } = string.Empty;
        }
        public class ExecuteValidator : AbstractValidator<ExecuteRequestAdd>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.Firstname).NotEmpty().NotNull();
                RuleFor(x => x.Lastname).NotEmpty().NotNull();
                RuleFor(x => x.Subjects).NotEmpty().NotNull();
                RuleFor(x => x.Age).NotEmpty().NotNull();
                RuleFor(x => x.Phone).NotEmpty().NotNull();
                RuleFor(x => x.Marks).NotEmpty().NotNull();
            }
        }
        public class Handler : IRequestHandler<ExecuteRequestAdd>
        {
            private readonly IStudents students;
            public Handler(IStudents students)
            {
                this.students = students;
            }
            public async Task<Unit> Handle(ExecuteRequestAdd request, CancellationToken cancellationToken)
            {
                var reusult = await students.NewStudent(request.Firstname, request.Lastname, request.Subjects, request.Age, request.Phone, request.Marks);
                if (reusult > 0)
                {
                    return Unit.Value;
                }
                throw new HandlerException(HttpStatusCode.BadRequest,
                    new { message = "Error: Failed Insert new student" });
            }
        }
    }
}
