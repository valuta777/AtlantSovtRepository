namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AtlantSovtContext : DbContext
    {
        public AtlantSovtContext()
            : base("name=AtlantSovtContext")
        {
        }

        public virtual DbSet<Cargo> Cargoes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientBankDetail> ClientBankDetails { get; set; }
        public virtual DbSet<ClientContact> ClientContacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustomsAddress> CustomsAddresses { get; set; }
        public virtual DbSet<DownloadAddress> DownloadAddresses { get; set; }
        public virtual DbSet<Filter> Filters { get; set; }
        public virtual DbSet<FineForDelay> FineForDelays { get; set; }
        public virtual DbSet<Forwarder> Forwarders { get; set; }
        public virtual DbSet<ForwarderBankDetail> ForwarderBankDetails { get; set; }
        public virtual DbSet<ForwarderContact> ForwarderContacts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDeny> OrderDenies { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<RegularyDelay> RegularyDelays { get; set; }
        public virtual DbSet<TaxPayerStatu> TaxPayerStatus { get; set; }
        public virtual DbSet<Transporter> Transporters { get; set; }
        public virtual DbSet<TransporterBankDetail> TransporterBankDetails { get; set; }
        public virtual DbSet<TransporterContact> TransporterContacts { get; set; }
        public virtual DbSet<TransporterVehicle> TransporterVehicles { get; set; }
        public virtual DbSet<UnCustomsAddress> UnCustomsAddresses { get; set; }
        public virtual DbSet<UploadAddress> UploadAddresses { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<WorkDocument> WorkDocuments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOptional(e => e.ClientBankDetail)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ClientContacts)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Transporters)
                .WithMany(e => e.Countries)
                .Map(m => m.ToTable("TransporterCountry").MapLeftKey("CountryId").MapRightKey("TransporterId"));

            modelBuilder.Entity<FineForDelay>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.FineForDelay)
                .HasForeignKey(e => e.FineForDelaysId);

            modelBuilder.Entity<Forwarder>()
                .HasOptional(e => e.ForwarderBankDetail)
                .WithRequired(e => e.Forwarder)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Forwarder>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Forwarder)
                .HasForeignKey(e => e.Forwarder1Id);

            modelBuilder.Entity<Forwarder>()
                .HasMany(e => e.Orders1)
                .WithOptional(e => e.Forwarder1)
                .HasForeignKey(e => e.Forwarder2Id);

            modelBuilder.Entity<Order>()
                .Property(e => e.Freight)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Payment)
                .HasForeignKey(e => e.PaymentTermsId);

            modelBuilder.Entity<RegularyDelay>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.RegularyDelay)
                .HasForeignKey(e => e.RegularyDelaysId);

            modelBuilder.Entity<TaxPayerStatu>()
                .HasMany(e => e.Clients)
                .WithOptional(e => e.TaxPayerStatu)
                .HasForeignKey(e => e.TaxPayerStatusId);

            modelBuilder.Entity<TaxPayerStatu>()
                .HasMany(e => e.Forwarders)
                .WithOptional(e => e.TaxPayerStatu)
                .HasForeignKey(e => e.TaxPayerStatusId);

            modelBuilder.Entity<TaxPayerStatu>()
                .HasMany(e => e.Transporters)
                .WithOptional(e => e.TaxPayerStatu)
                .HasForeignKey(e => e.TaxPayerStatusId);

            modelBuilder.Entity<Transporter>()
                .HasOptional(e => e.Filter)
                .WithRequired(e => e.Transporter);

            modelBuilder.Entity<Transporter>()
                .HasOptional(e => e.TransporterBankDetail)
                .WithRequired(e => e.Transporter);

            modelBuilder.Entity<Transporter>()
                .HasMany(e => e.TransporterContacts)
                .WithRequired(e => e.Transporter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.TransporterVehicles)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.TransportVehicleId)
                .WillCascadeOnDelete(false);
        }
    }
}
