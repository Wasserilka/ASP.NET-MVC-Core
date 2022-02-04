using Lesson_8.Models;
using Lesson_8.Data;

namespace Lesson_8.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        bool Update(Employee employee);
        bool Delete(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> Logger;
        private readonly ICollection<Employee> Employees;

        public EmployeeRepository(ILogger<EmployeeRepository> logger)
        {
            Logger = logger;
            Employees = TestData.Employees;
        }

        public bool Delete(Employee employee)
        {
            var old_employee = Employees.FirstOrDefault(employee);
            if (old_employee == null) return false;

            Employees.Remove(employee);
            return true;
        }

        public IEnumerable<Employee> GetAll() => Employees;

        public Employee? GetById(int id)
        {
            var employee = Employees.FirstOrDefault(i => i.Id == id);
            return employee;
        }

        public bool Update(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            var old_employee = Employees.FirstOrDefault(employee);
            if (old_employee == null) return false;

            old_employee.Name = employee.Name;
            old_employee.Position = employee.Position;
            old_employee.Age = employee.Age;
            old_employee.CreatedDate = employee.CreatedDate;

            return true;
        }
    }
}
