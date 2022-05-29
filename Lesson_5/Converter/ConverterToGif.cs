namespace Converter
{
    public class ConverterToGif : ConverterLogger, IConverter
    {
        public async void Convert(byte[] Value, string FileNameOutput)
        {
            var FileName = $"{FileNameOutput}.gif";

            using (FileStream FileStream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                await FileStream.WriteAsync(Value, 0, Value.Length);
            }
        }
    }
}