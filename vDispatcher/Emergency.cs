using System.Collections.Generic;

namespace vDispatcher
{
    public enum EmergencyType { ratunkowa, transportowa }

    public class Emergency : Service
    {
        public EmergencyType emergencyType { get; set; }
        public int stretcherNumber { get; set; }

        public Emergency(EmergencyType emergencyType, 
            List<IncType> capabilities, int actionTimer, int stretcherNumber)
        {
            this.emergencyType = emergencyType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.stretcherNumber = stretcherNumber;
        }
    }
}
