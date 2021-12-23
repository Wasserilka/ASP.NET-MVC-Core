using System.Windows;

namespace FileManager.ModalWindows
{
    public partial class ObjectInfoWindow : Window
    {
        public ObjectInfoWindow(string ObjectPath)
        {
            InitializeComponent();

            ObjectInfoTextBlock.Text = ObjectPath;
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
