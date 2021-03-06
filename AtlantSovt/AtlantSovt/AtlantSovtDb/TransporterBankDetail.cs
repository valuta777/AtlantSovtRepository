namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TransporterBankDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(200)]
        public string BankName { get; set; }

        [StringLength(200)]
        public string MFO { get; set; }

        [StringLength(200)]
        public string AccountNumber { get; set; }

        [StringLength(200)]
        public string EDRPOU { get; set; }

        [StringLength(200)]
        public string IPN { get; set; }

        [StringLength(200)]
        public string CertificateSerial { get; set; }

        [StringLength(200)]
        public string CertificateNamber { get; set; }

        [StringLength(200)]
        public string SWIFT { get; set; }

        [StringLength(200)]
        public string IBAN { get; set; }

        public virtual Transporter Transporter { get; set; }
    }
}
