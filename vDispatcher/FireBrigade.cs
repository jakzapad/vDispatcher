using System.Collections.Generic;

namespace vDispatcher
{
    public enum FireBrigadeType { drogowa, gasnicza }

    public class FireBrigade : Service
    {
        public FireBrigadeType fireBrigadeType { get; set; }
        public int tankCapacity { get; set; }

        public FireBrigade(FireBrigadeType fireBrigadeType, 
            List<IncType> capabilities, int actionTimer, int tankCapacity)
        {
            this.fireBrigadeType = fireBrigadeType;
            this.capabilities = capabilities;
            this.actionTimer = actionTimer;
            this.tankCapacity = tankCapacity;
        }
    }
}
