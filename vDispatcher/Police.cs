using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace vDispatcher
{
    public class Police : Service
    {
        public PoliceType policeType { get; set; }
        public int passengersNumber { get; set; }

        public Police(PoliceType policeType, List<IncType> capabilities, DispatcherTimer actionTimer, int passengersNumber)
        {
            this.policeType = policeType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.passengersNumber = passengersNumber;

            actionTimer.Interval = new TimeSpan(0, 0, 133);//na sztywno czas ma byc

        }
    }
}
