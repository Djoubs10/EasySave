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
        Finished
    }
    public class Transfer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Progress { get; set; }
        public States State { get; set; }
        public Transfer( string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Progress = 0;
            State = States.Ready;
        }
        public void Start()
        {
            State = States.Trasfering;
            Random rnd = new Random();
            while (Progress < 100)
            {
                int rand = rnd.Next(1, 25);
                Progress += rand;
                Thread.Sleep(rnd.Next(500, 2000));
            }
            State = States.Finished;
            Thread.Sleep(1000);
            State = States.Ready;
        }
    }
}
