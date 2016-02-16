namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            ClientContacts = new HashSet<ClientContact>();
            Contracts = new HashSet<Contract>();
            CustomsAddresses = new HashSet<CustomsAddress>();
            DownloadAddresses = new HashSet<DownloadAddress>();
            Orders = new HashSet<Order>();
            UnCustomsAddresses = new HashSet<UnCustomsAddress>();
            UploadAddresses = new HashSet<UploadAddress>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(200)]
        public string PhysicalAddress { get; set; }

        [StringLength(200)]
        public string GeografphyAddress { get; set; }

        public long? WorkDocumentId { get; set; }

        public long? TaxPayerStatusId { get; set; }

        public bool? ContractType { get; set; }

        [StringLength(50)]
        public string ContractNumber { get; set; }

        public string Comment { get; set; }

        public virtual TaxPayerStatu TaxPayerStatu { get; set; }

        public virtual WorkDocument WorkDocument { get; set; }

        public virtual ClientBankDetail ClientBankDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientContact> ClientContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomsAddress> CustomsAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DownloadAddress> DownloadAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnCustomsAddress> UnCustomsAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UploadAddress> UploadAddresses { get; set; }
    }
}
