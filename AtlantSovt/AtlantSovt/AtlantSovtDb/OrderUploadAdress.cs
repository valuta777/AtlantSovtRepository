namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderUploadAdress
    {
        public long Id { get; set; }

        public long AddressId { get; set; }

        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual UploadAddress UploadAddress { get; set; }
    }
}
