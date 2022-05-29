using System.IO;

namespace FileManager.DataGridModels.ObjectModels
{
    public class GridObjectHardDrive : GridObject
    {
        private DriveType _type;
        public DriveType Type { get { return _type; } }
        public GridObjectHardDrive(string name, DriveType type) : base(name)
        {
            _type = type;
        }
    }
}
