using EasySave.Helpers;
using System;
using System.Threading;

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
        private CancellationTokenSource _token;
        public string Id { get; set; }
        public string Name { get; set; }
        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }
        public States State {
            get => _state;
            set { _state = value; OnPropertyChanged();}
        }
        public Transfer( string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Progress = 0;
            _state = States.Ready;
            _token = new CancellationTokenSource(); 
        }
        public void Cancel()
        {
            State = States.Canceled;
            _token.Cancel();
        }
        public void Start()
        {
            State = States.Trasfering;
            Random rnd = new Random();
            while (Progress < 100)
            {
                if (_token.Token.IsCancellationRequested)
                {
                    _token = new CancellationTokenSource();
                    Thread.Sleep(1000);
                    Progress = 0;
                    State = States.Ready;
                    return;
                }
                int rand = rnd.Next(1, 25);
                if (Progress + rand > 100)
                    Progress = 100;
                else 
                    Progress += rand;
                Thread.Sleep(rnd.Next(500, 2000));
            }
            
            State = States.Finished;
            Thread.Sleep(1000);
            Progress = 0;
            State = States.Ready;
        }
    }
}
