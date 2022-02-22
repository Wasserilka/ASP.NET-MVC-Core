using System.IO;

namespace FileManager.Template
{
    public class TemplateModel
    {
        public string Name { get; set; }
        public long Free { get; set; }
        public long Full { get; set; }
        public DriveType Type { get; set; }
    }
}