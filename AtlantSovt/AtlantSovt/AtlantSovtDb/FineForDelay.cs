namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FineForDelay
    {
        public FineForDelay()
        {
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
