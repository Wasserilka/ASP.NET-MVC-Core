using Microsoft.AspNetCore.Mvc;
using Lesson_8.Models;
using Lesson_8.Data;
using Lesson_8.ViewModels;
using Lesson_8.Repositories;

namespace Lesson_8.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IEmployeeRepository EmployeeRepository;

        public EmployeeController(ILogger<HomeController> logger, IEmployeeRepository repository)
        {
            Logger = logger;
            EmployeeRepository = repository;
        }

        public IActionResult Index()
        {
            var employees = EmployeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Edit(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee == null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Age = employee.Age,
                CreatedDate = employee.CreatedDate
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Position = model.Position,
                CreatedDate = model.CreatedDate,
                Age = model.Age
            };

            EmployeeRepository.Update(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee == null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Age = employee.Age,
                CreatedDate = employee.CreatedDate
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            EmployeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee == null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Age = employee.Age,
                CreatedDate = employee.CreatedDate
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var employee = new Employee
            {
                Name = model.Name,
                Position = model.Position,
                CreatedDate = model.CreatedDate,
                Age = model.Age
            };

            EmployeeRepository.Create(employee);

            return RedirectToAction("Index");
        }
    }
}
