namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomsAddress")]
    public partial class CustomsAddress
    {
        public CustomsAddress()
        {
            OrderCustomsAddresses = new HashSet<OrderCustomsAddress>();
        }

        public long Id { get; set; }

        public long? CountryId { get; set; }

        [StringLength(50)]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string CityCode { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        [StringLength(50)]
        public string StreetName { get; set; }

        [StringLength(10)]
        public string HouseNumber { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        public long? ClientId { get; set; }

        [StringLength(100)]
        public string ShortRoute { get; set; }

        public virtual Client Client { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<OrderCustomsAddress> OrderCustomsAddresses { get; set; }
    }
}
