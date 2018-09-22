using System;

using Model.Interface;

namespace Model
{
    [Serializable]
    public class Employee : IEmployee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int ManagerId { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
