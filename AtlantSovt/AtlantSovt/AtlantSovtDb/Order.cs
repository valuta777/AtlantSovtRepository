namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long Id { get; set; }

        public bool? YorU { get; set; }

        public long? ClientId { get; set; }

        public long? Forwarder1Id { get; set; }

        public long? Forwarder2Id { get; set; }

        public long? TransporterId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DownloadDate { get; set; }

        public long? DownloadAddressId { get; set; }

        public long? CustomsAddressId { get; set; }

        public long? UnCustomsAddressId { get; set; }

        public long? UploadAddressId { get; set; }

        public long? CargoId { get; set; }

        public double? CargoWeight { get; set; }

        [StringLength(20)]
        public string LoadingForm1 { get; set; }

        [StringLength(20)]
        public string LoadingForm2 { get; set; }

        public int? ADRNumber { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [StringLength(50)]
        public string Freight { get; set; }

        public long? PaymentTermsId { get; set; }

        public long? AdditionalTermsId { get; set; }

        public long? RegularyDelaysId { get; set; }

        public long? FineForDelaysId { get; set; }

        public long? OrderDenyId { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual Client Client { get; set; }

        public virtual CustomsAddress CustomsAddress { get; set; }

        public virtual DownloadAddress DownloadAddress { get; set; }

        public virtual FineForDelay FineForDelay { get; set; }

        public virtual Forwarder Forwarder { get; set; }

        public virtual Forwarder Forwarder1 { get; set; }

        public virtual OrderDeny OrderDeny { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual RegularyDelay RegularyDelay { get; set; }

        public virtual Transporter Transporter { get; set; }

        public virtual UnCustomsAddress UnCustomsAddress { get; set; }

        public virtual UploadAddress UploadAddress { get; set; }
    }
}
