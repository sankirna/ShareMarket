using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

      

        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Manager> Managers { get; set; }

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

            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().Property(dp => dp.Name).IsRequired();
            modelBuilder.Entity<Manager>().HasKey(ma => ma.ManagerCode);
            modelBuilder.Entity<Manager>().Property(ma => ma.Name)
                .IsConcurrencyToken(true)
                .IsVariableLength()
                .HasMaxLength(20);

            modelBuilder.Entity<Manager>()
        .HasRequired(d => d.Department)
        .WithMany()
        .HasForeignKey(d => d.DepartmentId)
        .WillCascadeOnDelete();
        }
    }
}
