namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderLoadingForm")]
    public partial class OrderLoadingForm
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long LoadingFormId { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool IsFirst { get; set; }

        public virtual LoadingForm LoadingForm { get; set; }

        public virtual Order Order { get; set; }
    }
}
