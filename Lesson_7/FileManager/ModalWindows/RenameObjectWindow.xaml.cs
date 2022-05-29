using System.Windows;

namespace FileManager.ModalWindows
{
    public partial class RenameObjectWindow : Window
    {
        public RenameObjectWindow(string ObjectPath)
        {
            InitializeComponent();

            ObjectNameTextBox.Text = ObjectPath;
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string GetNewName()
        {
            return ObjectNameTextBox.Text;
        }
    }
}
