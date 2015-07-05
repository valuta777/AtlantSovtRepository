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
        public Forwarder()
        {
            ForwarderContacts = new HashSet<ForwarderContact>();
            ForwarderOrders = new HashSet<ForwarderOrder>();
            TransporterForwarderContracts = new HashSet<TransporterForwarderContract>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(50)]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        public string GeographyAddress { get; set; }

        public long? WorkDocumentId { get; set; }

        public long? TaxPayerStatusId { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "image")]
        public byte[] image { get; set; }

        public virtual TaxPayerStatu TaxPayerStatu { get; set; }

        public virtual WorkDocument WorkDocument { get; set; }

        public virtual ForwarderBankDetail ForwarderBankDetail { get; set; }

        public virtual ICollection<ForwarderContact> ForwarderContacts { get; set; }

        public virtual ICollection<ForwarderOrder> ForwarderOrders { get; set; }

        public virtual ICollection<TransporterForwarderContract> TransporterForwarderContracts { get; set; }
    }
}
