using FileManager.DataGridModels.ObjectModels;
using System.Collections.Generic;
using System.Windows.Data;

namespace FileManager.DataGridModels.ViewModels
{
    public class DataGridHardDriveView : IDataGridView<GridObjectHardDrive>
    {
        private CollectionViewSource _view;
        private List<GridObjectHardDrive> _source;
        public CollectionViewSource ViewList
        {
            get { return _view; }
        }

        public List<GridObjectHardDrive> SourceList
        {
            get { return _source; }
            set { _source = value; }
        }

        public DataGridHardDriveView()
        {
            _view = new CollectionViewSource();
            _source = new List<GridObjectHardDrive>();
            _view.Source = _source;
        }
    }
}
