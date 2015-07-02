namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransporterForwarderContract")]
    public partial class TransporterForwarderContract
    {
        public long Id { get; set; }

        public long TransporterId { get; set; }

        public long ForwarderId { get; set; }

        public long? ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataBegin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataEnd { get; set; }

        public bool? PorZ { get; set; }

        public byte? Language { get; set; }

        public virtual Forwarder Forwarder { get; set; }

        public virtual Transporter Transporter { get; set; }
    }
}
