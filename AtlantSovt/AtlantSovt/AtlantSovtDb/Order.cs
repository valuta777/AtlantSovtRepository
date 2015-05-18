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
        public Order()
        {
            ForwarderOrders = new HashSet<ForwarderOrder>();
            OrderCustomsAddresses = new HashSet<OrderCustomsAddress>();
            OrderDownloadAddresses = new HashSet<OrderDownloadAddress>();
            OrderUnCustomsAddresses = new HashSet<OrderUnCustomsAddress>();
            OrderUploadAdresses = new HashSet<OrderUploadAdress>();
        }

        public long Id { get; set; }

        public long? AdrTurId { get; set; }

        public bool? YorU { get; set; }

        public long? ClientId { get; set; }

        public long? TransporterId { get; set; }

        public DateTime? Date { get; set; }

        public long? TrailerId { get; set; }

        public long? CubeId { get; set; }

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

        public virtual AdditionalTerm AdditionalTerm { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual Client Client { get; set; }

        public virtual Cube Cube { get; set; }

        public virtual FineForDelay FineForDelay { get; set; }

        public virtual ICollection<ForwarderOrder> ForwarderOrders { get; set; }

        public virtual TirCmr TirCmr { get; set; }

        public virtual OrderDeny OrderDeny { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual RegularyDelay RegularyDelay { get; set; }

        public virtual Trailer Trailer { get; set; }

        public virtual Transporter Transporter { get; set; }

        public virtual ICollection<OrderCustomsAddress> OrderCustomsAddresses { get; set; }

        public virtual ICollection<OrderDownloadAddress> OrderDownloadAddresses { get; set; }

        public virtual ICollection<OrderUnCustomsAddress> OrderUnCustomsAddresses { get; set; }

        public virtual ICollection<OrderUploadAdress> OrderUploadAdresses { get; set; }
    }
}
