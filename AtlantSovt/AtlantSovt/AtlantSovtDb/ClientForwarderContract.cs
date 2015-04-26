namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientForwarderContract")]
    public partial class ClientForwarderContract
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ClientId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ForwarderId { get; set; }

        [StringLength(15)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataBegin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataEnd { get; set; }

        public virtual Client Client { get; set; }

        public virtual Forwarder Forwarder { get; set; }
    }
}
