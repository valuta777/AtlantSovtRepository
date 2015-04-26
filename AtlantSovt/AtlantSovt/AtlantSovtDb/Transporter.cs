namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transporter")]
    public partial class Transporter
    {
        public Transporter()
        {
            Orders = new HashSet<Order>();
            TransporterContacts = new HashSet<TransporterContact>();
            TransporterCountries = new HashSet<TransporterCountry>();
            TransporterForwarderContracts = new HashSet<TransporterForwarderContract>();
            TransporterVehicles = new HashSet<TransporterVehicle>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(30)]
        public string ShortName { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(50)]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        public string GeographyAddress { get; set; }

        public long? WorkDocumentId { get; set; }

        public long? TaxPayerStatusId { get; set; }

        public string Comment { get; set; }

        public bool? ContractType { get; set; }

        public virtual Filter Filter { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual TaxPayerStatu TaxPayerStatu { get; set; }

        public virtual WorkDocument WorkDocument { get; set; }

        public virtual TransporterBankDetail TransporterBankDetail { get; set; }

        public virtual ICollection<TransporterContact> TransporterContacts { get; set; }

        public virtual ICollection<TransporterCountry> TransporterCountries { get; set; }

        public virtual ICollection<TransporterForwarderContract> TransporterForwarderContracts { get; set; }

        public virtual ICollection<TransporterVehicle> TransporterVehicles { get; set; }
    }
}
