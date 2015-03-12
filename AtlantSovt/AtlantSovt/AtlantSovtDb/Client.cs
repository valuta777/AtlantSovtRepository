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
        public Client()
        {
            ClientContacts = new HashSet<ClientContact>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(50)]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
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

        public virtual ICollection<ClientContact> ClientContacts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
