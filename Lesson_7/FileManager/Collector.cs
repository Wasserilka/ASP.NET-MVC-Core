using FileManager.DataGridModels.ObjectModels;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    public static class Collector
    {
        public static void GetHardDrives(List<GridObjectHardDrive> List)
        {
            List.Clear();
            foreach(DriveInfo Drive in DriveInfo.GetDrives())
            {
                List.Add(new GridObjectHardDrive(Drive.Name, Drive.DriveType));
            }
        }
        public static void GetDirectories(List<GridObjectDirectory> List, string Path)
        {
            if (!Directory.Exists(Path))
            {
                return;
            }

            var Directories = Directory.GetDirectories(Path);
            List.Clear();

            foreach (string Dir in Directories)
            {
                var Info = new DirectoryInfo(Dir);
                List.Add(new GridObjectDirectory(Info.Name, DirectoryObjectType.Directory));
            }
            foreach (string File in Directory.GetFiles(Path))
            {
                List.Add(new GridObjectDirectory(System.IO.Path.GetFileName(File), DirectoryObjectType.File));
            }
        }
    }
}
