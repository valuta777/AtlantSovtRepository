namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Filter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public bool? IfForwarder { get; set; }

        public bool? TUR { get; set; }

        public bool? CMR { get; set; }

        public bool? EKMT { get; set; }

        public bool? Zborny { get; set; }

        public bool? AD { get; set; }

        public virtual Transporter Transporter { get; set; }
    }
}
