using System.Collections.Generic;
using System.Threading.Tasks;

using Model.Interface;

namespace Service.Interface
{
    public interface IEmployeeServiceSolr<T> where T : IEmployee
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
        IEnumerable<T> GetEmployeeByName(string name);
    }
}
