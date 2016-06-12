using System.Collections.Generic;

namespace vDispatcher
{
    public abstract class Service
    {
        public List<IncType> capabilities { get; set; }
        public int actionTimer { get; set; }
    }
}
