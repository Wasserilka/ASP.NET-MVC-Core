using System.Windows;
using System.Text;

namespace FileManager.ModalWindows
{
    public partial class CreateObjectWindow : Window
    {
        public CreateObjectWindow(bool IsFile, string ObjectPath)
        {
            InitializeComponent();

            if (IsFile)
            {
                PathTextBlock.Text = "Расположение файла:";
                NameTextBlock.Text = "Имя файла:";
            }
            else
            {
                PathTextBlock.Text = "Расположение папки:";
                NameTextBlock.Text = "Имя папки:";
            }

            PathTextBox.Text = ObjectPath;
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string GetFullPath()
        {
            var StrBuilder = new StringBuilder();
            StrBuilder.Append(PathTextBox.Text);
            StrBuilder.Append(NameTextBox.Text);
            return StrBuilder.ToString();
        }
    }
}
