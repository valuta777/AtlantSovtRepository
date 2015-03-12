namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransporterCountry")]
    public partial class TransporterCountry
    {
        public long Id { get; set; }

        public long? TransporterId { get; set; }

        public long? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Transporter Transporter { get; set; }
    }
}
