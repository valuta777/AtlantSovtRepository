namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientContact")]
    public partial class ClientContact
    {
        public long Id { get; set; }

        public long? ClientId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string TelephoneNumber { get; set; }

        [StringLength(50)]
        public string FaxNumber { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        public virtual Client Client { get; set; }
    }
}
