using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace vDispatcher
{
    public class Emergency : Service
    {
        public EmergencyType emergencyType { get; set; }
        public int stretcherNumber { get; set; }

        public Emergency(EmergencyType emergencyType, List<IncType> capabilities, DispatcherTimer actionTimer, int stretcherNumber)
        {
            this.emergencyType = emergencyType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.stretcherNumber = stretcherNumber;

            actionTimer.Interval = new TimeSpan(0, 0, 133);//na sztywno czas ma byc
        }
    }
}
