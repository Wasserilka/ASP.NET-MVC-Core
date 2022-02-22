using System;

namespace Lesson_4
{
    public class Model
    {
        public DateTimeOffset Date { get; set; }
        public string DayTemperature { get; set; }
        public string NightTemperature { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{Date}, {DayTemperature}, {NightTemperature}, {Description}";
    }
}
