using Lesson_6;
using Converter;
using Autofac;
using Autofac.Extras.AggregateService;

var Builder = new ContainerBuilder();

Builder.RegisterAggregateService<IScanConverterService>();
Builder.RegisterType<ConverterToBmp>().As<IConverter>();
Builder.RegisterType<Scanner>().As<IScanner>();
Builder.RegisterType<ScanConverter>();

IContainer Container = Builder.Build();
ScanConverter ScanConverter = Container.Resolve<ScanConverter>();

try
{
    await ScanConverter.ScanAndSave(@"newimage");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Сканирование завершено");
