using System.Data.Entity;
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

        public DbSet<Trader> Traders  { get; set; }

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

          
        }
    }
}
