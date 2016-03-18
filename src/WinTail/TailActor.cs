using System.IO;
using System.Text;
using Akka.Actor;

namespace WinTail
{
    class TailActor : UntypedActor
    {
        readonly IActorRef reporterActor;
        readonly string filePath;
        readonly FileObserver fileObserver;
        readonly Stream fileStream;
        readonly StreamReader fileStreamReader;

        public TailActor(IActorRef reporterActor, string filePath)
        {
            this.reporterActor = reporterActor;
            this.filePath = filePath;

            fileObserver = new FileObserver(Self, Path.GetFullPath(filePath));
            fileObserver.Start();

            fileStream = new FileStream(Path.GetFullPath(filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fileStreamReader = new StreamReader(fileStream, Encoding.UTF8);

            string fileText = fileStreamReader.ReadToEnd();
            Self.Tell(new InitialRead(filePath, fileText));
        }

        protected override void OnReceive(object message)
        {
            if (message is FileWrite)
            {
                string fileText = fileStreamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(fileText))
                {
                    reporterActor.Tell(fileText);
                }
            }
            else if (message is FileError)
            {
                FileError fileError = message as FileError;

                reporterActor.Tell($"Tail error: { fileError.Reason }.");
            }
            else if (message is InitialRead)
            {
                InitialRead initialRead = message as InitialRead;

                reporterActor.Tell(initialRead.Text);
            }
        }

        #region Message Types

        public class FileWrite
        {
            public FileWrite(string fileName)
            {
                FileName = fileName;
            }

            public string FileName { get; }
        }

        public class FileError
        {
            public FileError(string fileName, string reason)
            {
                FileName = fileName;
                Reason = reason;
            }

            public string FileName { get; }
            public string Reason { get; }
        }

        class InitialRead
        {
            public InitialRead(string fileName, string text)
            {
                FileName = fileName;
                Text = text;
            }

            public string FileName { get; }
            public string Text { get; }
        }

        #endregion
    }
}