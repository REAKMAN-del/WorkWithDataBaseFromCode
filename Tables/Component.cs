using System;
using System.Collections.Generic;

namespace DataBase.Tables
{
    public class Component
    {
        public Guid ComponentID { get; set; }

        public int CategoryID { get; set; }

        public string ComponentName { get; set; }

        public decimal Price { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool InStock { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<BuildComponent> BuildComponents { get; set; }
    }
}
