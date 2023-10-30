using EasySave.Helpers;
using System;
using System.Threading;
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
        Finished
    }
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
        public void Start()
        {
            State = States.Trasfering;
            Random rnd = new Random();
            while (Progress < 100)
            {
                Thread.Sleep(rnd.Next(500, 2000));
                _pause.WaitOne();
                if (_token.Token.IsCancellationRequested)
                {
                    Finish(true);
                    return;
                }
                int rand = rnd.Next(1, 25);
                if (Progress + rand > 100)
                    Progress = 100;
                else 
                    Progress += rand;
            }
            Finish();
        }
        public void Finish(bool canceled = false)
        {
            State = canceled ? States.Canceled : States.Finished;
            _token = new CancellationTokenSource();
            _pause = new ManualResetEvent(true);
            Thread.Sleep(1000);
            Progress = 0;
            State = States.Ready;
        }
    }
}
