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
            OrderLoadingForms = new HashSet<OrderLoadingForm>();
            OrderUnCustomsAddresses = new HashSet<OrderUnCustomsAddress>();
            OrderUploadAdresses = new HashSet<OrderUploadAdress>();
            TrackingComments = new HashSet<TrackingComment>();
        }

        public long Id { get; set; }

        public long? TirCmrId { get; set; }

        [StringLength(1)]
        public string YorU { get; set; }

        public long? ClientId { get; set; }

        public long? TransporterId { get; set; }

        public DateTime? Date { get; set; }

        public long? TrailerId { get; set; }

        public long? CubeId { get; set; }

        public DateTime? DownloadDate { get; set; }

        public long? CargoId { get; set; }

        public double? CargoWeight { get; set; }

        public int? ADRNumber { get; set; }

        public DateTime? UploadDate { get; set; }

        [StringLength(50)]
        public string Freight { get; set; }

        public long? PaymentTermsId { get; set; }

        public long? AdditionalTermsId { get; set; }

        public long? RegularyDelaysId { get; set; }

        public long? FineForDelaysId { get; set; }

        public long? OrderDenyId { get; set; }

        public bool? State { get; set; }

        public long? IndexNumber { get; set; }

        public byte? Language { get; set; }

        public virtual AdditionalTerm AdditionalTerm { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual Client Client { get; set; }

        public virtual Cube Cube { get; set; }

        public virtual FineForDelay FineForDelay { get; set; }

        public virtual ICollection<ForwarderOrder> ForwarderOrders { get; set; }

        public virtual OrderDeny OrderDeny { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual RegularyDelay RegularyDelay { get; set; }

        public virtual TirCmr TirCmr { get; set; }

        public virtual Trailer Trailer { get; set; }

        public virtual Transporter Transporter { get; set; }

        public virtual ICollection<OrderCustomsAddress> OrderCustomsAddresses { get; set; }

        public virtual ICollection<OrderDownloadAddress> OrderDownloadAddresses { get; set; }

        public virtual ICollection<OrderLoadingForm> OrderLoadingForms { get; set; }

        public virtual ICollection<OrderUnCustomsAddress> OrderUnCustomsAddresses { get; set; }

        public virtual ICollection<OrderUploadAdress> OrderUploadAdresses { get; set; }

        public virtual ICollection<TrackingComment> TrackingComments { get; set; }
    }
}
