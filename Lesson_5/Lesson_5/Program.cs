using Lesson_5;
using Converter;

var ScanOption = new ConverterToBmp();
var ScanProcess = new ScanConverter(new Scanner(), ScanOption);

try
{
    await ScanProcess.ScanAndSave(@"newimage");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Сканирование завершено");
