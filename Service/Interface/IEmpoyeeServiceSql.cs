using Model.Interface;

namespace Service.Interface
{
    public interface IEmpoyeeServiceSql<T> where T : IEmployee
    {
        bool CreateEmployee(IEmployee employee);
    }
}
