using System;
using System.Threading.Tasks;
using System.Threading;


namespace Lesson_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Semaphore = new SemaphoreSlim(3);
            for (int i = 0; i < 10; i++)
            {
                var Num = i;
                Task.Run(() =>
                {
                    Semaphore.Wait();
                    try
                    {
                        TaskFunc(Num);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        Semaphore.Release();
                    }
                });
            }
            Console.ReadLine();
        }

        static private void TaskFunc(int ThreadNum)
        {
            Console.WriteLine($"Поток {ThreadNum} - начало");
            Thread.Sleep(3000);
            Console.WriteLine($"Поток {ThreadNum} - конец");
        }
    }
}
