namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForwarderOrder")]
    public partial class ForwarderOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long ForwarderId { get; set; }

        public long OrderId { get; set; }

        public bool isFirst { get; set; }

        public virtual Forwarder Forwarder { get; set; }

        public virtual Order Order { get; set; }
    }
}
