using EasySave.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EasySave.Models
{
    public enum States
    {
        Ready,
        Trasfering,
        Paused,
        Canceled,
        Modifying,
        Finished,
        Error
    }
    public class Transfer : Notify
    {
        private int _progress;
        private States _state;
        private string _name, _target, _source;
        private CancellationTokenSource _token;
        private ManualResetEvent _pause;
        private static object _dirList = new object();
        private static object _transferFile = new object();
        public string Id { get; set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Source { get => _source; set { _source = value; OnPropertyChanged(); } }
        public string Target { get => _target; set { _target = value; OnPropertyChanged(); } }

        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }
        public States State {
            get => _state;
            set { _state = value; OnPropertyChanged();}
        }
        public Transfer( string name, string source, string target)
        {
            Id = Guid.NewGuid().ToString();
            _name = name; _source = source; _target = target; Progress = 0;
            _state = States.Ready; 
            _token = new CancellationTokenSource(); 
            _pause = new ManualResetEvent(true);
        }
        public void Cancel()
        {
            State = States.Canceled;
            _pause.Set();
            _token.Cancel();
        }
        public void Pause()
        {
            State = States.Paused;
            _pause.Reset();
        }
        public void Resume()
        {
            State = States.Trasfering;
            _pause.Set();
        }
        public void Start()
        {
            if (!Directory.Exists(Source))
            {
                State = States.Error;
                Finish();
                return;
            }
            if (!Directory.Exists(Target))
            {
                Directory.CreateDirectory(Target);
            }
            State = States.Trasfering;
            List<string> files = ListFiles(Source);
            for(int i = 0; i < files.Count; i++)
            {
                _pause.WaitOne();
                if (_token.Token.IsCancellationRequested)
                {
                    Finish();
                    return;
                }
                string sourceFile = files[i];
                string targetFile = Target + sourceFile.Substring(Source.Length);
                try
                {
                    lock (_transferFile)
                    {
                        string? dir = Path.GetDirectoryName(targetFile);
                        if (dir is null)
                            return;
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        File.Copy(sourceFile, targetFile, true);
                    }
                } catch (UnauthorizedAccessException) { }
                finally
                {
                    Progress = (int)Math.Floor((double)i / files.Count * 100);
                }
            }
            State = States.Finished;
            Finish();
        }

        private List<string> ListFiles(string directory)
        {
            List<string> files = new List<string>();
            lock (_dirList)
            {
                 files.AddRange(Directory.GetFiles(directory));
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    files.AddRange(ListFiles(dir));
                }
            }

            return files;
        }
        public void Finish()
        {
            _token = new CancellationTokenSource();
            _pause = new ManualResetEvent(true);
            Thread.Sleep(1000);
            Progress = 0;
            State = States.Ready;
        }
    }
}
