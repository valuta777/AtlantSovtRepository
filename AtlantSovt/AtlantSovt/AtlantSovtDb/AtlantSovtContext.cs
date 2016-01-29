namespace AtlantSovt.AtlantSovtDb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AtlantSovtContext : DbContext
    {
        public AtlantSovtContext()
           : base(ConnectionForm.GetConnectionString())
        {
        }

        public virtual DbSet<AdditionalTerm> AdditionalTerms { get; set; }
        public virtual DbSet<Arbeiten> Arbeitens { get; set; }
        public virtual DbSet<Cargo> Cargoes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientBankDetail> ClientBankDetails { get; set; }
        public virtual DbSet<ClientContact> ClientContacts { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Cube> Cubes { get; set; }
        public virtual DbSet<CustomsAddress> CustomsAddresses { get; set; }
        public virtual DbSet<DocumentCounter> DocumentCounters { get; set; }
        public virtual DbSet<DownloadAddress> DownloadAddresses { get; set; }
        public virtual DbSet<Filter> Filters { get; set; }
        public virtual DbSet<FineForDelay> FineForDelays { get; set; }
        public virtual DbSet<Forwarder> Forwarders { get; set; }
        public virtual DbSet<ForwarderBankDetail> ForwarderBankDetails { get; set; }
        public virtual DbSet<ForwarderContact> ForwarderContacts { get; set; }
        public virtual DbSet<ForwarderContract> ForwarderContracts { get; set; }
        public virtual DbSet<ForwarderOrder> ForwarderOrders { get; set; }
        public virtual DbSet<ForwarderStamp> ForwarderStamps { get; set; }
        public virtual DbSet<LoadingForm> LoadingForms { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCounter> OrderCounters { get; set; }
        public virtual DbSet<OrderCustomsAddress> OrderCustomsAddresses { get; set; }
        public virtual DbSet<OrderDeny> OrderDenies { get; set; }
        public virtual DbSet<OrderDownloadAddress> OrderDownloadAddresses { get; set; }
        public virtual DbSet<OrderLoadingForm> OrderLoadingForms { get; set; }
        public virtual DbSet<OrderUnCustomsAddress> OrderUnCustomsAddresses { get; set; }
        public virtual DbSet<OrderUploadAdress> OrderUploadAdresses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<RegularyDelay> RegularyDelays { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TaxPayerStatu> TaxPayerStatus { get; set; }
        public virtual DbSet<TirCmr> TirCmrs { get; set; }
        public virtual DbSet<TrackingComment> TrackingComments { get; set; }
        public virtual DbSet<Trailer> Trailers { get; set; }
        public virtual DbSet<Transporter> Transporters { get; set; }
        public virtual DbSet<TransporterBankDetail> TransporterBankDetails { get; set; }
        public virtual DbSet<TransporterContact> TransporterContacts { get; set; }
        public virtual DbSet<TransporterCountry> TransporterCountries { get; set; }
        public virtual DbSet<TransporterVehicle> TransporterVehicles { get; set; }
        public virtual DbSet<UnCustomsAddress> UnCustomsAddresses { get; set; }
        public virtual DbSet<UploadAddress> UploadAddresses { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<WorkDocument> WorkDocuments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalTerm>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.AdditionalTerm)
                .HasForeignKey(e => e.AdditionalTermsId);

            modelBuilder.Entity<Client>()
                .HasOptional(e => e.ClientBankDetail)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ClientContacts)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.CustomsAddresses)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.DownloadAddresses)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.UnCustomsAddresses)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.UploadAddresses)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ForwarderContracts)
                .WithOptional(e => e.Contract)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CustomsAddress>()
                .HasMany(e => e.OrderCustomsAddresses)
                .WithRequired(e => e.CustomsAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<DownloadAddress>()
                .HasMany(e => e.OrderDownloadAddresses)
                .WithRequired(e => e.DownloadAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<FineForDelay>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.FineForDelay)
                .HasForeignKey(e => e.FineForDelaysId);

            modelBuilder.Entity<Forwarder>()
                .HasOptional(e => e.ForwarderBankDetail)
                .WithRequired(e => e.Forwarder)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Forwarder>()
                .HasMany(e => e.ForwarderOrders)
                .WithOptional(e => e.Forwarder)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Forwarder>()
                .HasOptional(e => e.ForwarderStamp)
                .WithRequired(e => e.Forwarder)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoadingForm>()
                .HasMany(e => e.OrderLoadingForms)
                .WithRequired(e => e.LoadingForm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasOptional(e => e.Arbeiten)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ForwarderOrders)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderCustomsAddresses)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDownloadAddresses)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderLoadingForms)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderUnCustomsAddresses)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderUploadAdresses)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.TrackingComments)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

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
                .WithRequired(e => e.Transporter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Transporter>()
                .HasOptional(e => e.TransporterBankDetail)
                .WithRequired(e => e.Transporter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UnCustomsAddress>()
                .HasMany(e => e.OrderUnCustomsAddresses)
                .WithRequired(e => e.UnCustomsAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<UploadAddress>()
                .HasMany(e => e.OrderUploadAdresses)
                .WithRequired(e => e.UploadAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.TransporterVehicles)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.TransportVehicleId)
                .WillCascadeOnDelete(false);
        }
    }
}
