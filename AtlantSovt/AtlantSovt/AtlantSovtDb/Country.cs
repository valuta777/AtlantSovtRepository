namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            CustomsAddresses = new HashSet<CustomsAddress>();
            DownloadAddresses = new HashSet<DownloadAddress>();
            TransporterCountries = new HashSet<TransporterCountry>();
            UnCustomsAddresses = new HashSet<UnCustomsAddress>();
            UploadAddresses = new HashSet<UploadAddress>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<CustomsAddress> CustomsAddresses { get; set; }

        public virtual ICollection<DownloadAddress> DownloadAddresses { get; set; }

        public virtual ICollection<TransporterCountry> TransporterCountries { get; set; }

        public virtual ICollection<UnCustomsAddress> UnCustomsAddresses { get; set; }

        public virtual ICollection<UploadAddress> UploadAddresses { get; set; }
    }
}
