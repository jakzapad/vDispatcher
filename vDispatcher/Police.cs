using System.Collections.Generic;

namespace vDispatcher
{
    public enum PoliceType { drogowka, kryminalna }

    public class Police : Service
    {
        public PoliceType policeType { get; set; }
        public int passengersNumber { get; set; }

        public Police(PoliceType policeType, 
            List<IncType> capabilities, int actionTimer, int passengersNumber)
        {
            this.policeType = policeType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.passengersNumber = passengersNumber;
        }
    }
}
