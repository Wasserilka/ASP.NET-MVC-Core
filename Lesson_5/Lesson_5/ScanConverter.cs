using Converter;

namespace Lesson_5
{
    public class ScanConverter
    {
        private IScanner _Scanner;
        private IConverter _Converter;

        public ScanConverter(IScanner Scanner, IConverter Converter)
        {
            _Scanner = Scanner;
            _Converter = Converter;
        }

        public async Task ScanAndSave(string FileNameOutput)
        {
            var ScanResult = await _Scanner.Scan();
            _Converter.Convert(ScanResult.ScannedDocument, FileNameOutput);
            _Converter.Log(ScanResult.CPUUsage, ScanResult.HDDUsage, "log.txt");
        }
    }
}
