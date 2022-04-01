using DapperCRUD.Infrastructure.Interfaz;
using FluentValidation;
using MediatR;
using System.Net;

namespace DapperCRUD.Aplication.CRUD
{
    public class UpdateStudent
    {
        public class ExecuteUpdate : IRequest
        {
            public int IdStudent { get; set; }
            public string Firstname { get; set; } = string.Empty;
            public string Lastname { get; set; } = string.Empty;
            public string Subjects { get; set; } = string.Empty;
            public int Age { get; set; }
            public string Phone { get; set; } = string.Empty;
            public string Marks { get; set; } = string.Empty;
        }
        public class ExecuteValidator : AbstractValidator<ExecuteUpdate>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.IdStudent).NotEmpty().NotNull();
                RuleFor(x => x.Firstname).NotEmpty().NotNull();
                RuleFor(x => x.Lastname).NotEmpty().NotNull();
                RuleFor(x => x.Subjects).NotEmpty().NotNull();
                RuleFor(x => x.Age).NotEmpty().NotNull();
                RuleFor(x => x.Phone).NotEmpty().NotNull();
                RuleFor(x => x.Marks).NotEmpty().NotNull();
            }
        }
        public class Handler : IRequestHandler<ExecuteUpdate>
        {
            private readonly IStudents students;
            public Handler(IStudents students)
            {
                this.students = students;
            }
            public async Task<Unit> Handle(ExecuteUpdate request, CancellationToken cancellationToken)
            {
                var resultUpdate = await students.UpdateStudent(request.IdStudent, request.Firstname, request.Lastname, request.Subjects, request.Age, request.Phone, request.Marks);
                if (resultUpdate > 0)
                {
                    return Unit.Value;
                }
                throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });
            }
        }
    }
}
