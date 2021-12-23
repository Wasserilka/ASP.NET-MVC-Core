using FileManager.DataGridModels.ViewModels;
using System.Windows.Controls;

namespace FileManager.DataGridModels.GridModels
{
    public class DataGridHardDrive : DataGrid
    {
        private DataGridHardDriveView _view;

        public DataGridHardDrive()
        {
            _view = new DataGridHardDriveView();
            Collector.GetHardDrives(_view.SourceList);
            ItemsSource = _view.ViewList.View;
        }
    }
}
