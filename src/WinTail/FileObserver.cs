using System;
using System.IO;
using Akka.Actor;

namespace WinTail
{
    class FileObserver : IDisposable
    {
        readonly IActorRef tailActor;
        readonly string absoluteFilePath;
        readonly string fileDirectory;
        readonly string fileName;

        FileSystemWatcher fileWatcher;

        public FileObserver(IActorRef tailActor, string absoluteFilePath)
        {
            this.tailActor = tailActor;
            this.absoluteFilePath = absoluteFilePath;
            this.fileDirectory = Path.GetDirectoryName(absoluteFilePath);
            this.fileName = Path.GetFileName(absoluteFilePath);
        }

        public void Start()
        {
            fileWatcher = new FileSystemWatcher(fileDirectory, fileName);
            fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            fileWatcher.Changed += OnFileChanged;
            fileWatcher.Error += OnFileError;
            fileWatcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            fileWatcher.Dispose();
        }

        void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                tailActor.Tell(new TailActor.FileWrite(e.Name), ActorRefs.NoSender);
            }
        }

        void OnFileError(object sender, ErrorEventArgs e)
        {
            tailActor.Tell(new TailActor.FileError(fileName, e.GetException().Message), ActorRefs.NoSender);
        }
    }
}