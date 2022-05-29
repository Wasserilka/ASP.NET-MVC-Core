namespace Converter
{
    public abstract class ConverterLogger
    {
        public void Log(long HDDUsage, long CPUUsage, string LogPath)
        {
            File.AppendAllText(LogPath, $"[{DateTime.Now:g}] - HDD: {HDDUsage}, CPU: {CPUUsage}\n");
        }
    }
}
