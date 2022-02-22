using FileManager.DataGridModels.ObjectModels;
using System.Collections.Generic;
using System.Windows.Data;

namespace FileManager.DataGridModels.ViewModels
{
    public class DataGridDirectoryView : IDataGridView<GridObjectDirectory>
    {
        private CollectionViewSource _view;
        private List<GridObjectDirectory> _source;
        public CollectionViewSource ViewList
        {
            get { return _view; }
        }

        public List<GridObjectDirectory> SourceList
        {
            get { return _source; }
            set { _source = value; }
        }

        public DataGridDirectoryView()
        {
            _view = new CollectionViewSource();
            _source = new List<GridObjectDirectory>();
            _view.Source = _source;
        }
    }
}
