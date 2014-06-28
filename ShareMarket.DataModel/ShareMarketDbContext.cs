using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ShareMarket.Core;


namespace ShareMarket.DataAccess
{
    public class ShareMarketDbContext : DbContext
    {
        public ShareMarketDbContext()
            : base("dbConnection")
        {
            Database.SetInitializer<ShareMarketDbContext>(new DropCreateDatabaseIfModelChanges<ShareMarketDbContext>());
            //Database.SetInitializer<ShareMarketDbContext>(new DropCreateDatabaseAlways<ShareMarketDbContext>());
            //Database.SetInitializer<ShareMarketDbContext>(new CreateDatabaseIfNotExists<ShareMarketDbContext>());
            //Database.SetInitializer<ShareMarketDbContext>(new  ShareMarketDbInitializer());


            //Disable initializer
            //Database.SetInitializer<ShareMarketDbContext>(null);
        }


        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public DbSet<Roles> Roleses { get; set; }
        public DbSet<UsersRole> UsersRoles { get; set; }

        public DbSet<Trader> Traders { get; set; }

        public DbSet<QueuedEmail> QueuedEmails { get; set; }
        public DbSet<QueuedSms> QueuedSmses { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<ScheduleTask> ScheduleTasks { get; set; }

        public DbSet<Setting> Settings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////dynamically load all configuration
            //System.Type configType = typeof(ProductMap);   //any of your configuration classes here
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
            //.Where(type => !String.IsNullOrEmpty(type.Namespace))
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}
            ////...or do it manually below. For example,
            ////modelBuilder.Configurations.Add(new LanguageMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trader>()
                    .HasRequired(s => s.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByUserId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trader>()
                .HasRequired(s => s.UpdatedByUser)
                .WithMany()
                .HasForeignKey(x => x.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            // SetCascadingDelete(modelBuilder,new Trader());
        }

        public void SetCascadingDelete<T>(DbModelBuilder modelBuilder, T enity) where T : class
        {


            modelBuilder.Entity<T>()
                 .HasRequired(x => x.GetType().GetProperty("CreatedByUser"))
                 .WithMany()
                 .HasForeignKey(x => x.GetType().GetProperty("CreatedByUserId").Name)
                 .WillCascadeOnDelete(false);
        }
    }
}
