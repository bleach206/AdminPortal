using System.Collections.Generic;
using System.Threading.Tasks;

using Model.Interface;

namespace Repository.Interface
{
    public interface IEmployeeRepositorySolr<T> where T : IEmployee
    {
        /// <summary>
        /// get list of all employees
        /// </summary>
        /// <returns>IEnumerable of type generic</returns>
        Task<IEnumerable<T>> GetEmployee();

        /// <summary>
        /// get employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEmployeeByName(string name);
    }
}
