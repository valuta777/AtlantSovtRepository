namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForwarderContract")]
    public partial class ForwarderContract
    {
        public long Id { get; set; }

        public long? ForwarderId { get; set; }

        public long? ContractId { get; set; }

        public int IsFirst { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Forwarder Forwarder { get; set; }
    }
}
