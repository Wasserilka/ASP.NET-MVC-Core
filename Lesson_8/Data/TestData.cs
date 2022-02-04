using Lesson_8.Models;

namespace Lesson_8.Data
{
    public class TestData
    {
        public static ICollection<Employee> Employees { get; } = Enumerable.Range(1, 20)
            .Select(i => new Employee
            {
                Id = i,
                Name = $"Employee {i}",
                Position = "Some Position",
                Age = new Random().Next(20, 60),
                CreatedDate = DateTime.Now
            })
            .ToList();
    }
}
