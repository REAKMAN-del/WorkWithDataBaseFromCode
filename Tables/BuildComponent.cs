using System;

namespace DataBase.Tables
{
    public class BuildComponent
    {
        public int BuildID { get; set; }

        public Guid ComponentID { get; set; }

        public int Quantity { get; set; }

        public PС_Build PcBuild { get; set; }

        public Component Component { get; set; }
    }
}
