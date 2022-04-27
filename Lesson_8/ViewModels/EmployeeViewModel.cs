using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_8.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Display(Name = "Должность")]
        [StringLength(50, MinimumLength = 3)]
        public string Position { get; set; }
        [Display(Name = "Возраст")]
        [Range(18, 70)]
        public int Age { get; set; }
        [Display(Name = "Дата приема")]
        public DateTimeOffset CreatedDate { get; set; }
    }
}
