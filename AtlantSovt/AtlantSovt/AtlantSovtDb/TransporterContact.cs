namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransporterContact")]
    public partial class TransporterContact
    {
        public long Id { get; set; }

        public long TransporterId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string TelephoneNumber { get; set; }

        [StringLength(50)]
        public string FaxNumber { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        public virtual Transporter Transporter { get; set; }
    }
}
