using Lesson_8.Models;
using Lesson_8.Data;
using Dapper;

namespace Lesson_8.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        void Create (Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> Logger;

        public EmployeeRepository(ILogger<EmployeeRepository> logger)
        {
            Logger = logger;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(Employee employee)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<Employee>("INSERT INTO employees(name, position, age, createddate) VALUES(@name, @position, @age, @createddate)",
                    new { name = employee.Name, position = employee.Position, age = employee.Age, createddate = employee.CreatedDate.ToUnixTimeSeconds() });
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<Employee>("DELETE FROM employees WHERE id=@id",
                    new { id = id });
            }
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<Employee>("SELECT * FROM employees").AsList();
            }
        }

        public Employee? GetById(int id)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<Employee>("SELECT * FROM employees WHERE id=@id",
                    new { id = id });
            }
        }

        public bool Update(Employee employee)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<Employee>("UPDATE employees SET name=@name, position=@position, age=@age, createddate=@createddate WHERE id=@id",
                   new { id = employee.Id, name = employee.Name, position = employee.Position, age = employee.Age, createddate = employee.CreatedDate.ToUnixTimeSeconds() });
            }

            return true;
        }
    }
}
