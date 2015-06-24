namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoadingForm")]
    public partial class LoadingForm
    {
        public LoadingForm()
        {
            OrderLoadingForms = new HashSet<OrderLoadingForm>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public virtual ICollection<OrderLoadingForm> OrderLoadingForms { get; set; }
    }
}
