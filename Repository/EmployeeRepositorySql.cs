using System;
using System.Data.SqlClient;

using Dapper;

using Model.Interface;
using Repository.Interface;

namespace Repository
{
    public class EmployeeRepositorySql : IEmployeeRepositorySql<IEmployee>
    {
        #region Field

        private readonly string _connection;
        #endregion

        #region Constructor

        public EmployeeRepositorySql(string connection)
        {
            _connection = connection;
        }
        #endregion

        #region Method
        public bool CreateEmployee(IEmployee employee)
        {
            try
            {
                using (var cnn = new SqlConnection(_connection))
                {
                    var sql = @"INSERT INTO work..EmployeeDetails (FullName, ManagerId, DateOfJoining) VALUES (@FullName, @ManagerId, @DateOfJoining) SELECT CAST(SCOPE_IDENTITY() AS INT)";
                    var insert = cnn.Execute(sql, new { employee.FullName, employee.ManagerId, DateOfJoining = DateTime.Now.ToUniversalTime() });
                    return insert > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion        
    }
}
