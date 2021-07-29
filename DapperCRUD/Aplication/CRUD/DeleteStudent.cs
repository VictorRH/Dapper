using DapperCRUD.Infrastructure.Interfaz;
using FluentValidation;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DapperCRUD.Aplication.CRUD
{
    public class DeleteStudent
    {

        public class ExecuteDelete : IRequest
        {
            public int Id { get; set; }
        }

        public class ExecuteValidator : AbstractValidator<ExecuteDelete>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<ExecuteDelete>
        {
            private readonly IStudents students;

            public Handler(IStudents students)
            {
                this.students = students;
            }

            public async Task<Unit> Handle(ExecuteDelete request, CancellationToken cancellationToken)
            {
                var resultDelete = await students.DeleteStudent(request.Id);

                if (resultDelete > 0)
                {
                    return Unit.Value;
                }
                throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });

            }
        }
    }
}
