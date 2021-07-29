using Dapper;
using DapperCRUD.Core;
using DapperCRUD.Core.Persistence.DapperConnection;
using DapperCRUD.Infrastructure.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperCRUD.Infrastructure.Repository
{
    public class RepositoryStudent : IStudents
    {
        private readonly IFactoryConnection factoryConnection;

        public RepositoryStudent(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }

        public async Task<int> DeleteStudent(int idStudentR)
        {
            var storedProcedure = "sp_deleteStudent";
            try
            {
                var connection = factoryConnection.GetConnection();

                var result = await connection.ExecuteAsync(storedProcedure, new
                {
                    idstudent = idStudentR
                }, commandType: CommandType.StoredProcedure);
                factoryConnection.CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Failed register new students", ex);
            }

        }

        public async Task<IEnumerable<StudentModel>> GetAllStudents()
        {
            IEnumerable<StudentModel> studentList = null;
            var storedProcedure = "sp_getStudents";
            try
            {
                var connection = factoryConnection.GetConnection();
                studentList = await connection.QueryAsync<StudentModel>(storedProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Get List Students", ex);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return studentList;

        }

        public async Task<StudentModel> GetStudentId(int idStudentR)
        {
            var storedProcedure = "sp_getStudentId";
            try
            {
                var connection = factoryConnection.GetConnection();
                StudentModel student = await connection.QueryFirstAsync<StudentModel>(storedProcedure, new
                {
                    idStudent = idStudentR
                }, commandType: CommandType.StoredProcedure);

                factoryConnection.CloseConnection();
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: student not found", ex);
            }
        }

        public async Task<int> NewStudent(string firstnameR, string lastnameR, string subjectsR, int ageR, string phoneR, string markR)
        {
            var storedProcedure = "sp_newstudent";
            try
            {
                var connection = factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(storedProcedure, new
                {
                    firstname = firstnameR,
                    lastname = lastnameR,
                    subjects = subjectsR,
                    age = ageR,
                    phone = phoneR,
                    mark = markR,
                    datecreated = DateTime.Now

                }, commandType: CommandType.StoredProcedure);

                factoryConnection.CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: error insert new student", ex);
            }
        }

        public async Task<int> UpdateStudent(int idStudentR, string firstnameR, string lastnameR, string subjectsR, int ageR, string phoneR, string markR)
        {
            var storedProcedure = "sp_updateStudent";

            try
            {
                var connection = factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(storedProcedure, new
                {
                    idstudent = idStudentR,
                    firstname = firstnameR,
                    lastname = lastnameR,
                    subjects = subjectsR,
                    age = ageR,
                    phone = phoneR,
                    mark = markR
                }, commandType: CommandType.StoredProcedure);

                factoryConnection.CloseConnection();

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error: error update student", ex);
            }
        }
    }
}
