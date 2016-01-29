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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transporter()
        {
            Contracts = new HashSet<Contract>();
            Orders = new HashSet<Order>();
            TransporterContacts = new HashSet<TransporterContact>();
            TransporterCountries = new HashSet<TransporterCountry>();
            TransporterVehicles = new HashSet<TransporterVehicle>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(30)]
        public string ShortName { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(200)]
        public string PhysicalAddress { get; set; }

        [StringLength(200)]
        public string GeographyAddress { get; set; }

        public long? WorkDocumentId { get; set; }

        public long? TaxPayerStatusId { get; set; }

        public string Comment { get; set; }

        public bool? ContractType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual Filter Filter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual TaxPayerStatu TaxPayerStatu { get; set; }

        public virtual WorkDocument WorkDocument { get; set; }

        public virtual TransporterBankDetail TransporterBankDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransporterContact> TransporterContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransporterCountry> TransporterCountries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransporterVehicle> TransporterVehicles { get; set; }
    }
}
