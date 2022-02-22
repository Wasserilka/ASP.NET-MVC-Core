namespace FileManager.DataGridModels.ObjectModels
{
    public class GridObjectDirectory : GridObject
    {
        private DirectoryObjectType _type;
        public DirectoryObjectType Type { get { return _type; } }
        public GridObjectDirectory(string name, DirectoryObjectType type) : base(name)
        {
            _type = type;
        }
    }

    public enum DirectoryObjectType
    {
        Directory,
        File
    }
}
