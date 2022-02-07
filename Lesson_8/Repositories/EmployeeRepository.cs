using Lesson_8.Models;
using Lesson_8.Data;

namespace Lesson_8.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        int Create (Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
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

        public int Create(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            if (Employees.Contains(employee)) throw new ArgumentException($"Сотрудник {nameof(employee)} уже существует");

            var newId = Employees.Count == 0 ? 1: Employees.Max(x => x.Id) + 1;
            employee.Id = newId;
            Employees.Add(employee);

            return newId;
        }

        public bool Delete(int id)
        {
            var employee = Employees.FirstOrDefault(i => i.Id == id);
            if (employee == null) return false;

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
