using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MessageCommand Command_1;
        public MessageCommand Command_2;

        public MainWindow()
        {
            InitializeComponent();

            LeftButton.DataContext = this;

            var Factory = new CommandFactory();
            Command_1 = Factory.CreateCommand("Текст на левой кнопке");
            Command_2 = Factory.CreateCommand("Текст на правой кнопке");
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            Command_1.Execute(((Button)sender).Content);
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            Command_2.Execute(((Button)sender).Content);
        }
    }
}
