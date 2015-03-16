namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle")]
    public partial class Vehicle
    {
        public Vehicle()
        {
            TransporterVehicles = new HashSet<TransporterVehicle>();
        }

        public long Id { get; set; }

        [StringLength(30)]
        public string Type { get; set; }

        public virtual ICollection<TransporterVehicle> TransporterVehicles { get; set; }
    }
}
