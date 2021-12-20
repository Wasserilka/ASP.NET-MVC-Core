namespace Converter
{
    public interface IConverter
    {
        void Convert(byte[] Value, string FileNameOutput);
        void Log(long CPUUsage, long HDDUsage, string LogPath);
    }
}
