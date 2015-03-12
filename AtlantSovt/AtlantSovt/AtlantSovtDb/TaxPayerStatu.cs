namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaxPayerStatu
    {
        public TaxPayerStatu()
        {
            Clients = new HashSet<Client>();
            Forwarders = new HashSet<Forwarder>();
            Transporters = new HashSet<Transporter>();
        }

        public long Id { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Forwarder> Forwarders { get; set; }

        public virtual ICollection<Transporter> Transporters { get; set; }
    }
}
