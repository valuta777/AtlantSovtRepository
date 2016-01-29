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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public long? ClientId { get; set; }

        public long? TransporterId { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? Date { get; set; }

        public long? TrailerId { get; set; }

        public long? StaffId { get; set; }

        public long? CubeId { get; set; }

        public DateTime? DownloadDateFrom { get; set; }

        public long? CargoId { get; set; }

        [StringLength(100)]
        public string CargoWeight { get; set; }

        public int? ADRNumber { get; set; }

        public DateTime? UploadDateFrom { get; set; }

        public string Freight { get; set; }

        public long? PaymentTermsId { get; set; }

        public long? AdditionalTermsId { get; set; }

        public long? RegularyDelaysId { get; set; }

        public long? FineForDelaysId { get; set; }

        public long? OrderDenyId { get; set; }

        public bool? State { get; set; }

        public long? IndexNumber { get; set; }

        public byte? Language { get; set; }

        public string Note { get; set; }

        public DateTime? DownloadDateTo { get; set; }

        public DateTime? UploadDateTo { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual AdditionalTerm AdditionalTerm { get; set; }

        public virtual Arbeiten Arbeiten { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual Client Client { get; set; }

        public virtual Cube Cube { get; set; }

        public virtual FineForDelay FineForDelay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForwarderOrder> ForwarderOrders { get; set; }

        public virtual OrderDeny OrderDeny { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual RegularyDelay RegularyDelay { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual TirCmr TirCmr { get; set; }

        public virtual Trailer Trailer { get; set; }

        public virtual Transporter Transporter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderCustomsAddress> OrderCustomsAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDownloadAddress> OrderDownloadAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLoadingForm> OrderLoadingForms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderUnCustomsAddress> OrderUnCustomsAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderUploadAdress> OrderUploadAdresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrackingComment> TrackingComments { get; set; }
    }
}
