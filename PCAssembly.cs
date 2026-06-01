using DataBase.Tables;
using System.Data.Entity;

namespace DataBase
{
    public class PCAssembly : DbContext
    {
        private const string connectionString = "Server = PC\\SQLEXPRESS; Database = PCAssembly; Trusted_connection = True; TrustServerCertificate=True;";
        public PCAssembly() : base(connectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Component> Components { get; set; }

        public DbSet<BuildComponent> Build_Components { get; set; }

        public DbSet<PС_Build> PC_Builds { get; set; }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<Component>().ToTable("Components");
            modelBuilder.Entity<Component>()
                .Property(c => c.Price).HasPrecision(18, 2);

            modelBuilder.Entity<BuildComponent>().ToTable("Build_Components");
            modelBuilder.Entity<BuildComponent>()
                .HasKey(bc => new { bc.BuildID, bc.ComponentID });

            modelBuilder.Entity<PС_Build>().ToTable("PC_Builds");
            modelBuilder.Entity<PС_Build>()
                .Property(b => b.TotalPrice).HasPrecision(18, 2);

            modelBuilder.Entity<Client>().ToTable("Clients");

            modelBuilder.Entity<Component>()
                .HasRequired(c => c.Category)
                .WithMany(cat => cat.Components)
                .HasForeignKey(c => c.CategoryID);

            modelBuilder.Entity<Client>()
                .HasOptional(cl => cl.ChosenBuild)
                .WithMany(b => b.Clients)
                .HasForeignKey(cl => cl.ChosenBuildID);

            modelBuilder.Entity<BuildComponent>()
                .HasRequired(bc => bc.PcBuild)
                .WithMany(b => b.BuildComponents)
                .HasForeignKey(bc => bc.BuildID);

            modelBuilder.Entity<BuildComponent>()
                .HasRequired(bc => bc.Component)
                .WithMany(c => c.BuildComponents)
                .HasForeignKey(bc => bc.ComponentID);
        }
    }
}
