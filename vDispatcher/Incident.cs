using System;
using System.Windows.Threading;

namespace vDispatcher
{
    public enum IncType { wypadek, pozar, kradziez, pobicie, zabojstwo, transport }
    
    public class Incident
    {
        public IncType incType { get; set; }
        public int woundedNumber { get; set; }
        public int deadNumber { get; set; }
        public string description { get; set; }
        public Random randTimeInc { get; set; }
        public int timeInc { get; set; }
        public DispatcherTimer incTimer { get; set; }
        public int endTimer { get; set; }

        public Incident(IncType incType, int woundedNumber, int deadNumber, 
            string description, DispatcherTimer incTimer)
        {
            this.incType = incType;
            this.woundedNumber = woundedNumber;
            this.deadNumber = deadNumber;
            this.description = description;
            this.randTimeInc = new Random();
            this.timeInc = randTimeInc.Next(100, 300);
            this.incTimer = incTimer;
            startIncTimer(this.timeInc, this.incTimer);            
        }

        public void startIncTimer(int timeInc, DispatcherTimer incTimer)
        {
            incTimer.Tick += incTimer_Tick;
            incTimer.Interval = new TimeSpan(0, 0, timeInc);
            endTimer = timeInc;
            incTimer.Start();
        }

        private void incTimer_Tick(object sender, EventArgs e)
        {
            this.description = "[PRZETERMINOWANY]" + this.description;
            incTimer.Stop();
            incTimer = null;
        }
    }
}
