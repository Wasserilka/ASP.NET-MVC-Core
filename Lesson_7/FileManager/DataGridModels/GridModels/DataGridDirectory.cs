using FileManager.DataGridModels.ViewModels;
using System.Diagnostics;
using System.Windows.Controls;

namespace FileManager.DataGridModels.GridModels
{
    public class DataGridDirectory : DataGrid
    {
        private DataGridDirectoryView _view;

        public DataGridDirectory()
        {
            _view = new DataGridDirectoryView();
            ItemsSource = _view.ViewList.View;
        }
        public void GetDirectory(string path)
        {
            Collector.GetDirectories(_view.SourceList, path);
            _view.ViewList.View.Refresh();
        }

        public void ExecuteFile(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}
