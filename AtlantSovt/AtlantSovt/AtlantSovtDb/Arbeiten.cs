namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Arbeiten")]
    public partial class Arbeiten
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(100)]
        public string ClientAccountNumber { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(100)]
        public string ClientPayment { get; set; }

        public DateTime? DownloadDate { get; set; }

        [StringLength(100)]
        public string VehicleNumber { get; set; }

        [StringLength(100)]
        public string TransporterPayment { get; set; }

        public string Note { get; set; }

        public virtual Order Order { get; set; }
    }
}
