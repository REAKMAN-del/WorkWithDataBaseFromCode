using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Tables
{
    public class PС_Build
    {
        [Key]
        public int BuildID { get; set; }

        public string BuildName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsAssembled { get; set; }

        public virtual ICollection<BuildComponent> BuildComponents { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
