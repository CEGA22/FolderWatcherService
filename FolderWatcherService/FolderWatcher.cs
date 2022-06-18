using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace FolderWatcherService
{
    public class FolderWatcher
    {       
        public string path = @"C:\Folder1";
        EventLog eventLog = new EventLog();     
        public FolderWatcher()
        {         
            MonitorDirectory(path);
        }

        public void TimerElapsed(object? sender, ElapsedEventArgs e)
        {                          
            MonitorDirectory(path);
        }

        private static void MonitorDirectory(string path)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();           
            fileSystemWatcher.Path = path;
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName| NotifyFilters.DirectoryName | NotifyFilters.Attributes;
            fileSystemWatcher.Created += FileSystemWatcher_Created;          
            fileSystemWatcher.Deleted += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Created;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.EnableRaisingEvents = true;          
        }

        private static void WriteLine(string line)
        {
            try
            {
                File.AppendAllText(@"C:\Folder1\FolderWatch.txt", Environment.NewLine + line);
            }
            catch (Exception e)
            {
                var x = e.Message;               
            }
        }
   
        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine(e.ChangeType + " " + e.FullPath + " " + DateTime.Now.ToString());
                WriteLine(e.ChangeType + " " +  e.FullPath + " " + DateTime.Now.ToString());
            }
            catch (Exception c)
            {
                var x = c.Message;
            }
        }
        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                Console.WriteLine("File renamed: " + e.OldFullPath + " to " + e.FullPath + " " + DateTime.Now.ToString());
                WriteLine("File renamed: " + e.OldFullPath + " to " + e.FullPath + " " + DateTime.Now.ToString());
            }
            catch (Exception n )
            {
                var s = n.Message;
            }           
        }               
        public void Start()
        {
            eventLog.Source = "FolderWatcherService";
            eventLog.WriteEntry("This is a test message for start.", EventLogEntryType.Information);                       
        }

        public void Stop()
        {
            eventLog.Source = "FolderWatcherService";
            eventLog.WriteEntry("This is a test message for stopped.", EventLogEntryType.Information);               
        }      
    }
}
