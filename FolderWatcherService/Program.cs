// See https://aka.ms/new-console-template for more information
using FolderWatcherService;
using System.Diagnostics;
using Topshelf;

var exitCode = HostFactory.Run(x =>
{
    x.Service<FolderWatcher>(f =>
    {
        f.ConstructUsing(folderwatch => new FolderWatcher());
        f.WhenStarted(folderwatch => folderwatch.Start());
        f.WhenStopped(folderwatch => folderwatch.Stop());
    });

    x.RunAsLocalSystem();
    x.SetServiceName("FolderWatcherService");
    x.SetDisplayName("Folder Watcher Service");
    x.SetDescription("This is the sample folder watcher service used in a demo");
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.Exit(exitCodeValue);


