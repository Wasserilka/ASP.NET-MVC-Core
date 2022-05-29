namespace Lesson_5
{
    public interface IScanner
    {
        Task<ScanResultModel> Scan();
    }

    public class Scanner : IScanner
    {
        public async Task<ScanResultModel> Scan()
        {
            var ScanResult = new ScanResultModel();

            using (FileStream FileStream = File.OpenRead("image.jpg"))
            {
                var Bytes = new byte[FileStream.Length];
                await FileStream.ReadAsync(Bytes, 0, Bytes.Length);
                ScanResult.ScannedDocument = Bytes;
            }

            ScanResult.CPUUsage = new Random().Next(2 ^ 10 * 10, 2 ^ 10 * 50);
            ScanResult.HDDUsage = new Random().Next(2 ^ 10 * 100, 2 ^ 10 * 500);

            return ScanResult;
        }
    }
}
