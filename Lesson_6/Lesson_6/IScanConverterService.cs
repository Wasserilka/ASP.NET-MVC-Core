using Converter;

namespace Lesson_6
{
    public interface IScanConverterService
    {
        public IScanner Scanner { get; }

        public IConverter Converter { get; }
    }
}
