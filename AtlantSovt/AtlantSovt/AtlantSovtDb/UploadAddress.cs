namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UploadAddress")]
    public partial class UploadAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UploadAddress()
        {
            OrderUploadAdresses = new HashSet<OrderUploadAdress>();
        }

        public long Id { get; set; }

        public long? CountryId { get; set; }

        [StringLength(200)]
        public string CountryCode { get; set; }

        [StringLength(200)]
        public string CityCode { get; set; }

        [StringLength(200)]
        public string CityName { get; set; }

        [StringLength(200)]
        public string StreetName { get; set; }

        [StringLength(200)]
        public string HouseNumber { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(200)]
        public string ContactPerson { get; set; }

        public long? ClientId { get; set; }

        [StringLength(200)]
        public string ShortRoute { get; set; }

        public virtual Client Client { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderUploadAdress> OrderUploadAdresses { get; set; }
    }
}
