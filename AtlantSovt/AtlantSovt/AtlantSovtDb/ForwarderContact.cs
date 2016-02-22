namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForwarderContact")]
    public partial class ForwarderContact
    {
        public long Id { get; set; }

        public long ForwarderId { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string TelephoneNumber { get; set; }

        [StringLength(200)]
        public string FaxNumber { get; set; }

        [StringLength(200)]
        public string ContactPerson { get; set; }

        public virtual Forwarder Forwarder { get; set; }
    }
}
