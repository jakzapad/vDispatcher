using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace vDispatcher
{
    public enum PoliceType { drogowka, kryminalna}
    public enum FireBrigadeType { drogowa, gasnicza}
    public enum EmergencyType { ratunkowa, transportowa}

    public abstract class Service
    {
        public List<IncType> capabilities { get; set; }
        public DispatcherTimer actionTimer { get; set; }
    }


}
