using System;
using FileManager.ModalWindows;
using System.Windows;
using System.Text;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls;
using FileManager.Template;
using FileManager.DataGridModels.ObjectModels;

namespace FileManager.Commands
{
    public static class ReportCommands
    {
        public static readonly ICommand CreateReport = new RelayCommand(o =>
        {
            var HardDriveList = new List<TemplateModel>();

            foreach (DriveInfo Drive in DriveInfo.GetDrives())
            {
                HardDriveList.Add(new TemplateModel { 
                    Name = Drive.Name, 
                    Type = Drive.DriveType,
                    Free = Drive.TotalFreeSpace,
                    Full = Drive.TotalSize
                });
            }

            var Report = new HardDriveReport();
            Report.GenerateReport(HardDriveList);
        });
    }
}
