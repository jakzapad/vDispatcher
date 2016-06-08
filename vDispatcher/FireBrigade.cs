using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace vDispatcher
{
    public class FireBrigade : Service
    {
        public FireBrigadeType fireBrigadeType { get; set; }
        public int tankCapacity { get; set; }

        public FireBrigade(FireBrigadeType fireBrigadeType, List<IncType> capabilities, DispatcherTimer actionTimer, int tankCapacity)
        {
            this.fireBrigadeType = fireBrigadeType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.tankCapacity = tankCapacity;

            actionTimer.Interval = new TimeSpan(0, 0, 99);//na sztywno czas ma byc
        }
    }
}
