namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Forwarder")]
    public partial class Forwarder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Forwarder()
        {
            ForwarderContacts = new HashSet<ForwarderContact>();
            ForwarderContracts = new HashSet<ForwarderContract>();
            ForwarderOrders = new HashSet<ForwarderOrder>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(200)]
        public string PhysicalAddress { get; set; }

        [StringLength(200)]
        public string GeographyAddress { get; set; }

        public long? WorkDocumentId { get; set; }

        public long? TaxPayerStatusId { get; set; }

        public string Comment { get; set; }

        public virtual TaxPayerStatu TaxPayerStatu { get; set; }

        public virtual WorkDocument WorkDocument { get; set; }

        public virtual ForwarderBankDetail ForwarderBankDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForwarderContact> ForwarderContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForwarderContract> ForwarderContracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForwarderOrder> ForwarderOrders { get; set; }

        public virtual ForwarderStamp ForwarderStamp { get; set; }
    }
}
