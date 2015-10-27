namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForwarderStamp")]
    public partial class ForwarderStamp
    {
        public long Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Stamp { get; set; }

        public virtual Forwarder Forwarder { get; set; }
    }
}
