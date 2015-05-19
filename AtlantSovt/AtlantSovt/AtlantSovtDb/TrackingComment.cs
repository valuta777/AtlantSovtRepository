namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrackingComment")]
    public partial class TrackingComment
    {
        public long Id { get; set; }

        public string Comment { get; set; }

        public long OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
