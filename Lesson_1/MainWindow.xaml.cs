using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;


namespace Lesson_1
{
    public partial class MainWindow : Window
    {
        private List<Thread> FibonacciThreads;
        public double ThreadTicks { get; set; }
        private ThreadLocksList<string> FibonacciList;

        public MainWindow()
        {
            ThreadTicks = 1000;
            FibonacciList = new ThreadLocksList<string>();
            FibonacciThreads = new List<Thread>();

            InitializeComponent();
            ThreadTicksSlider.DataContext = this;

            var NewThread = new Thread(new ThreadStart(GetNumbers));
            FibonacciThreads.Add(NewThread);
            NewThread.Start();

            for (int i = 0; i <= 3; i++)
            {
                NewThread = new Thread(new ParameterizedThreadStart(GetNumbersForList));
                FibonacciThreads.Add(NewThread);
                NewThread.Start(i);
            }
        }

        private void GetNumbers()
        {
            var Number1 = 0;
            var Number2 = 1;
            var StrBuilder = new StringBuilder();
            StrBuilder.Append(Number1);
            RefreshText();

            while (true)
            {
                try
                {
                    Thread.Sleep((int)ThreadTicks);
                    SetFibonacci(ref Number1, ref Number2);
                    RefreshText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
            }

            void RefreshText()
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    StrBuilder.Append(Number2);
                    TextBlock.Text = StrBuilder.ToString();
                }));
            }
        }
        private void GetNumbersForList(object ThreadId)
        {
            var Number1 = 0;
            var Number2 = 1;
            var StrBuilder = new StringBuilder();
            FibonacciList.AddFromThread(Number1.ToString(), (int)ThreadId);
            FibonacciList.AddFromThread(Number2.ToString(), (int)ThreadId);

            while (true)
            {
                try
                {
                    Thread.Sleep((int)ThreadTicks);
                    SetFibonacci(ref Number1, ref Number2);
                    FibonacciList.AddFromThread(Number2.ToString(), (int)ThreadId);
                    StrBuilder.Clear();
                    lock (FibonacciList)
                    {
                        if (FibonacciList.Count > 5)
                        {
                            FibonacciList.RemoveAt(0);
                        }
                        FibonacciList.ForEach(o => StrBuilder.Append(o.ToString()));
                    }
                    RefreshText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
            }

            void RefreshText()
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    lock (TextBlock2)
                    {
                        TextBlock2.Text = StrBuilder.ToString();
                    }
                }));
            }
        }

        private void SetFibonacci(ref int Number1, ref int Number2)
        {
            var TempNumber = Number2;
            Number2 = Number1 + Number2;
            Number1 = TempNumber;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FibonacciThreads.ForEach(o => o.Interrupt());
        }
    }
}
