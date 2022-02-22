namespace FileManager.DataGridModels.ObjectModels
{
    public abstract class GridObject
    {
        private string _name;
        public string Name { get { return _name; } }
        public GridObject(string name)
        {
            _name = name;
        }
    }
}
