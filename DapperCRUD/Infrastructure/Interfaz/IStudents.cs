using DapperCRUD.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperCRUD.Infrastructure.Interfaz
{
    public interface IStudents
    {
        Task<int> NewStudent(string firstnameR, string lastnameR, string subjectsR, int ageR, string phoneR, string markR);
        Task<int> DeleteStudent(int idStudentR);
        Task<int> UpdateStudent(int idStudentR, string firstnameR, string lastnameR, string subjectsR, int ageR, string phoneR, string markR);
        Task<StudentModel> GetStudentId(int idStudentR);
        Task<IEnumerable<StudentModel>> GetAllStudents();


    }
}
