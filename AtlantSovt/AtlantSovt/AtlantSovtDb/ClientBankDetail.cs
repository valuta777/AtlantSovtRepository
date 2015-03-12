namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientBankDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string MFO { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string EDRPOU { get; set; }

        [StringLength(50)]
        public string IPN { get; set; }

        [StringLength(50)]
        public string CertificateNamber { get; set; }

        [StringLength(50)]
        public string SWIFT { get; set; }

        [StringLength(50)]
        public string IBAN { get; set; }

        public virtual Client Client { get; set; }
    }
}
