using System.Windows;
using System.Windows.Controls;

namespace Lesson_4
{
    public partial class MainWindow : Window
    {
        private Facade _Facade;
        private string UriHeader = "https://yandex.ru/pogoda/?";
        public MainWindow()
        {
            InitializeComponent();

            _Facade = new Facade();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var Uri = $"{UriHeader}lat={LatitudeBox.Text}&lon={LongitudeBox.Text}";
            try
            {
                var Collection = await _Facade.GetWeatherData(Uri);
                DataGrid.ItemsSource = Collection;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
