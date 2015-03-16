namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransporterVehicle")]
    public partial class TransporterVehicle
    {
        public long Id { get; set; }

        public long TransporterId { get; set; }

        public long TransportVehicleId { get; set; }

        public virtual Transporter Transporter { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
