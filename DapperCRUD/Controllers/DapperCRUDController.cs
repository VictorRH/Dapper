using DapperCRUD.Aplication.CRUD;
using DapperCRUD.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperCRUD.Controllers
{
    public class DapperCRUDController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> NewStudent(NewInsert.ExecuteRequestAdd data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateStudent(UpdateStudent.ExecuteUpdate data)
        {
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteStudent(int id)
        {
            return await Mediator.Send(new DeleteStudent.ExecuteDelete { Id = id });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudent(int id)
        {
            return await Mediator.Send(new GetIdStudent.ExeuteGetIdStudent { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentModel>>> GetAllStudents()
        {
            return await Mediator.Send(new AllStudents.ExecuteAllStudents());
        }

    }
}
