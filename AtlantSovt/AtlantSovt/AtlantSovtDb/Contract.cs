namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contract")]
    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            ForwarderContracts = new HashSet<ForwarderContract>();
        }

        public long Id { get; set; }

        public long? TransporterId { get; set; }

        public long? ClientId { get; set; }

        public long? ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataBegin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDataEnd { get; set; }

        public byte? Type { get; set; }

        public string TemplateName { get; set; }

        public bool? State { get; set; }

        public virtual Client Client { get; set; }

        public virtual Transporter Transporter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForwarderContract> ForwarderContracts { get; set; }
    }
}
