using System.Collections.Generic;
using System.Windows.Data;

namespace FileManager.DataGridModels.ViewModels
{
    public interface IDataGridView<T> where T : class
    {
        CollectionViewSource ViewList { get; }
        List<T> SourceList { get; set; }
    }
}
