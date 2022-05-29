using Microsoft.AspNetCore.Mvc;
using Lesson_8.Models;
using Lesson_8.Messenger;
using Lesson_8.Messenger.Configuration;
using Lesson_8.Messenger.Models;
using Lesson_8.ViewModels;
using Lesson_8.Repositories;
using AutoMapper;
using Razor.Templating.Core;

namespace Lesson_8.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IMapper Mapper;

        public EmployeeController(ILogger<HomeController> logger, IEmployeeRepository repository, IMapper mapper)
        {
            Logger = logger;
            EmployeeRepository = repository;
            Mapper = mapper;
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

            var model = Mapper.Map<EmployeeViewModel>(employee);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var employee = Mapper.Map<Employee>(model);
            EmployeeRepository.Update(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee == null) return NotFound();

            var model = Mapper.Map<EmployeeViewModel>(employee);
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

            var model = Mapper.Map<EmployeeViewModel>(employee);
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

            var employee = Mapper.Map<Employee>(model);
            EmployeeRepository.Create(employee);

            return RedirectToAction("Index");
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(Message model)
        {
            var mailConfig = new MailGatewayOptions();
            mailConfig.SenderName = "";
            mailConfig.Sender = "";
            mailConfig.Password = "";
            mailConfig.SMTPServer = "";

            var mail = new MailGateway(mailConfig);

            model.IsHtml = true;
            model.Body = RazorTemplateEngine.RenderAsync("~/MailTemplates/Message.cshtml", model).Result;

            mail.SendMessage(model);

            return RedirectToAction("Index");
        }
    }
}
