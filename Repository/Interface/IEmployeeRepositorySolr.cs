using System.Collections.Generic;

using Model.Interface;

namespace Repository.Interface
{
    public interface IEmployeeRepositorySolr<T> where T : IEmployee
    {
        /// <summary>
        /// get list of all employees
        /// </summary>
        /// <returns>IEnumerable of type generic</returns>
        IEnumerable<T> GetEmployee();

        /// <summary>
        /// get employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<T> GetEmployeeByName(string name);
    }
}
