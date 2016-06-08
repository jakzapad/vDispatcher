using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Incident(IncType rodzajInc, int iloscRannych, int iloscZabitych, string opis, DispatcherTimer incTimer)
        {
            this.incType = rodzajInc;
            this.woundedNumber = iloscRannych;
            this.deadNumber = iloscZabitych;
            this.description = opis;
            this.randTimeInc = new Random(); //incTimer OK, startIncTimer OK, randTimeInc NOK, timeInc NOK
            this.timeInc = randTimeInc.Next(2, 5);
            this.incTimer = incTimer;
            startIncTimer(this.timeInc, this.incTimer);            
        }

        public void startIncTimer(int timeInc, DispatcherTimer incTimer)
        {
            incTimer.Tick += incTimer_Tick;
            incTimer.Interval = new TimeSpan(0, 0, 100);//zmiana interawalu timeInc
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
