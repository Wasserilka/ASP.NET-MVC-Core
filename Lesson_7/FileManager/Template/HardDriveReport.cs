using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;
using System.IO;

namespace FileManager.Template
{
    public class HardDriveReport
    {
        public void GenerateReport(List<TemplateModel> TemplateList, string Output = "")
        {
            if (TemplateList.Count == 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Output))
            {
                Output = $@"{Directory.GetCurrentDirectory()}\HardDriveReport.docx";
            }

            if (File.Exists(Output))
            {
                File.Delete(Output);
            }

            File.Copy("HardDriveTemplate.docx", Output);

            List<TableRowContent> Table = new List<TableRowContent>();

            foreach (TemplateModel Model in TemplateList)
            {
                Table.Add(new TableRowContent(new List<FieldContent>()
                {
                    new FieldContent("Имя", Model.Name),
                    new FieldContent("Свободно", Model.Free.ToString()),
                    new FieldContent("Всего", Model.Full.ToString()),
                    new FieldContent("Тип", Model.Type.ToString())
                }));
            }

            var Values = new Content(TableContent.Create("HardDrives", Table));

            using (var OutputDocument = new TemplateProcessor(Output).SetRemoveContentControls(true))
            {
                OutputDocument.FillContent(Values);
                OutputDocument.SaveChanges();
            }
        }
    }
}
