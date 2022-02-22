using System;
using FileManager.ModalWindows;
using System.Windows;
using System.Text;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls;

namespace FileManager.Commands
{
    public static class DataGridCommands
    {
        private static MainWindow Main = (MainWindow)Application.Current.MainWindow;
        private static bool IsCut = false;

        public static readonly ICommand OpenDataGridObject = new RelayCommand(o =>
        {
            if (o is DataGridCell)
            {
                try
                {
                    OpenDirectory((TextBlock)((DataGridCell)o).Content);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        });
        public static readonly ICommand OpenHardDrive = new RelayCommand(o =>
        {
            var CellContent = (TextBlock)((DataGridCell)o).Content;

            try
            {
                Main.DataGridDirectory.GetDirectory(CellContent.Text);
                Main.TextBoxPath.Text = CellContent.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });
        public static readonly ICommand OpenDataGridObjectContext = new RelayCommand(o =>
        {
            if (o is Border)
            {
                try
                {
                    OpenDirectory(GetDataForContextMenu((Border)o));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        });
        public static readonly ICommand CopyObject = new RelayCommand(o =>
        {
            if (o is Border)
            {
                SetObjectToClipboard(GetPath(GetDataForContextMenu((Border)o).Text));
                IsCut = false;
            }
        });
        public static readonly ICommand CutObject = new RelayCommand(o =>
        {
            if (o is Border)
            {
                SetObjectToClipboard(GetPath(GetDataForContextMenu((Border)o).Text));
                IsCut = true;
            }
        });
        public static readonly ICommand PasteObject = new RelayCommand(o =>
        {
            var PathString = GetPath();
            var SourcePathString = Clipboard.GetText();

            try
            {
                if (Directory.Exists(SourcePathString))
                {
                    var DirInfo = new DirectoryInfo(SourcePathString);
                    var FullPath = GetPath(DirInfo.Name);

                    if (Directory.Exists(GetPath()) && !Directory.Exists(FullPath))
                    {
                        DirCopy(SourcePathString, FullPath);
                        if (IsCut)
                        {
                            DirDelete(SourcePathString);
                            IsCut = false;
                        }
                    }
                }
                else if (File.Exists(SourcePathString))
                {
                    var FullPath = Path.GetFileName(SourcePathString);

                    if (Directory.Exists(GetPath()) && !File.Exists(GetPath(FullPath)))
                    {
                        File.Copy(SourcePathString, FullPath);
                        if (IsCut)
                        {
                            File.Delete(SourcePathString);
                            IsCut = false;  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RefreshDataGrid();
            }
        });
        public static readonly ICommand DeleteObject = new RelayCommand(o =>
        {
            var PathString = GetPath(GetDataForContextMenu((Border)o).Text);

            if (o is Border)
            {
                try
                {
                    if (Directory.Exists(PathString))
                    {
                        DirDelete(PathString);
                    }
                    else if (File.Exists(PathString))
                    {
                        File.Delete(PathString);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                RefreshDataGrid();
            }
        });
        public static readonly ICommand CreateFile = new RelayCommand(o =>
        {
            var NewModalWindow = new CreateObjectWindow(true, Main.TextBoxPath.Text);
            NewModalWindow.Owner = Main;
            NewModalWindow.Title = "Создать файл";
            NewModalWindow.ShowDialog();
            if (NewModalWindow.DialogResult == true)
            {
                var FullPath = NewModalWindow.GetFullPath();
                if (!File.Exists(FullPath))
                {
                    try
                    {
                        File.Create(FullPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        RefreshDataGrid();
                    }
                }
            }
        });
        public static readonly ICommand CreateDirectory = new RelayCommand(o =>
        {
            var NewModalWindow = new CreateObjectWindow(false, Main.TextBoxPath.Text);
            NewModalWindow.Owner = Main;
            NewModalWindow.Title = "Создать папку";
            NewModalWindow.ShowDialog();
            if (NewModalWindow.DialogResult == true)
            {
                var FullPath = NewModalWindow.GetFullPath();
                if (!Directory.Exists(FullPath))
                {
                    try
                    {
                        Directory.CreateDirectory(FullPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        RefreshDataGrid();
                    }
                }
            }
        });
        public static readonly ICommand Refresh = new RelayCommand(o =>
        {
            RefreshDataGrid();
        });
        public static readonly ICommand RenameObject = new RelayCommand(o =>
        {
            var PathString = GetPath(GetDataForContextMenu((Border)o).Text);

            if (o is Border)
            {
                try
                {
                    if (Directory.Exists(PathString))
                    {
                        var DirInfo = new DirectoryInfo(PathString);
                        var NewModalWindow = new RenameObjectWindow(DirInfo.Name);
                        NewModalWindow.Owner = Main;
                        NewModalWindow.ShowDialog();
                        if (NewModalWindow.DialogResult == true)
                        {
                            DirInfo.MoveTo($@"{DirInfo.Parent}\{NewModalWindow.GetNewName()}");
                        }
                    }
                    else if (File.Exists(PathString))
                    {
                        var NewModalWindow = new RenameObjectWindow(Path.GetFileName(PathString));
                        NewModalWindow.Owner = Main;
                        NewModalWindow.ShowDialog();
                        if (NewModalWindow.DialogResult == true)
                        {
                            File.Move(PathString, $@"{Main.TextBoxPath.Text}\{NewModalWindow.GetNewName()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                RefreshDataGrid();
            }
        });
        public static readonly ICommand GetObjectInfo = new RelayCommand(o =>
        {
            var PathString = GetPath(GetDataForContextMenu((Border)o).Text);
            var ObjectInfo = "";

            if (o is Border)
            {
                try
                {
                    if (Directory.Exists(PathString))
                    {
                        var DirInfo = new DirectoryInfo(PathString);
                        ObjectInfo = $"Имя: {DirInfo.Name}\nРазмер: {GetDirLength(DirInfo.GetFiles("", SearchOption.AllDirectories))}\nДата создания: {DirInfo.CreationTime}";
                    }
                    else if (File.Exists(PathString))
                    {
                        var FileInfo = new FileInfo(PathString);
                        ObjectInfo = $"Имя: {FileInfo.Name}\nРазмер: {FileInfo.Length}\nДата создания: {FileInfo.CreationTime}";
                    }
                    var NewModalWindow = new ObjectInfoWindow(Path.GetFileName(ObjectInfo));
                    NewModalWindow.Owner = Main;
                    NewModalWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                RefreshDataGrid();
            }
        });

        private static string GetPath()
        {
            return Main.TextBoxPath.Text;
        }
        private static void SetObjectToClipboard(string ObjectPath)
        {
            if (Directory.Exists(ObjectPath) || File.Exists(ObjectPath))
            {
                Clipboard.SetText(ObjectPath);
            }
        }
        private static void RefreshDataGrid()
        {
            Main.DataGridDirectory.GetDirectory(Main.TextBoxPath.Text);
        }
        private static void DirDelete(string SourcePath)
        {
            foreach (string FilePath in Directory.GetFiles(SourcePath))
            {
                File.Delete(FilePath);
            }
            foreach (string DirPath in Directory.GetDirectories(SourcePath))
            {
                DirDelete(DirPath);
            }
            Directory.Delete(SourcePath);
        }
        private static void DirCopy(string SourcePath, string DistPath)
        {
            if (!Directory.Exists(DistPath))
            {
                Directory.CreateDirectory(DistPath);
            }
            foreach (string DirPath in Directory.GetDirectories(SourcePath))
            {
                var DirInfo = new DirectoryInfo(DirPath);
                var DirNameNew = $@"{DistPath}\{DirInfo.Name}";
                DirCopy(DirPath, DirNameNew);
            }
            foreach (string FilePath in Directory.GetFiles(SourcePath))
            {
                File.Copy(FilePath, $@"{DistPath}\{Path.GetFileName(FilePath)}", true);
            }
        }
        private static string GetPath(string ObjectPath)
        {
            var StringBuilder = new StringBuilder();

            StringBuilder.Append(Main.TextBoxPath.Text);
            StringBuilder.Append(ObjectPath);

            return StringBuilder.ToString();
        }
        private static void OpenDirectory(TextBlock CellContent)
        {
            var PathString = GetPath(CellContent.Text);

            if (Directory.Exists(PathString))
            {
                Main.DataGridDirectory.GetDirectory(PathString);
                Main.TextBoxPath.Text = $@"{PathString}\";
            }
            else if (File.Exists(PathString))
            {
                Main.DataGridDirectory.ExecuteFile(PathString);
            }
        }
        private static TextBlock GetDataForContextMenu(Border o)
        {
            return (TextBlock)((ContentPresenter)o.Child).Content;
        }

        static long GetDirLength(FileInfo[] Files)
        {
            long Sum = 0;
            foreach (FileInfo File in Files)
            {
                Sum += File.Length;
            }
            return Sum;
        }
    }
}
