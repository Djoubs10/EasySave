using EasySave.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace EasySave.Models
{
    public class Transfer : Notify
    {
        private int _progress;
        private States _state;
        private string _name, _target, _source;
        private CancellationTokenSource _token;
        private ManualResetEvent _pause;
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
        private bool CanStart()
        {
            if (!Directory.Exists(Source))
            {
                State = States.SourceNotFound;
                return false;
            }
            if (!Directory.Exists(Target))
                Directory.CreateDirectory(Target);
            State = States.Trasfering;
            return true;
        }
        public void Start()
        {
            if (CanStart())
            {
                List<string> files = FilesManager.GetFilesList(Source);
                for (int i = 0; i < files.Count; i++)
                {
                    _pause.WaitOne();
                    if (_token.Token.IsCancellationRequested)
                    {
                        Finish();
                        return;
                    }
                    try
                    {
                        FilesManager.CopyFile(files[i], Target + files[i].Substring(Source.Length));
                    }
                    catch { }
                    finally
                    {
                        Progress = (int)Math.Floor((double)i / files.Count * 100);
                    }
                }
                State = States.Finished;
            }
            Finish();
        }       
        private void Finish()
        {
            _token = new CancellationTokenSource();
            _pause = new ManualResetEvent(true);
            Thread.Sleep(1000);
            Progress = 0;
            State = States.Ready;
        }
    }
}
