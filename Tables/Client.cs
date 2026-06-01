using System;

namespace DataBase.Tables
{
    public class Client
    {
        public int ClientID { get; set; }

        public string ClientName { get; set; }

        public DateTime OrderDate { get; set; }

        public int? ChosenBuildID { get; set; }

        public PС_Build ChosenBuild { get; set; }
    }
}
