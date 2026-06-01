using System.Collections.Generic;

namespace DataBase.Tables
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
