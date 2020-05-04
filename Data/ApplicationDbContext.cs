using  ECommerceCMS.Data.Entity;
using ECommerceCMS.Data.Entity.MasterData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace  ECommerceCMS.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CMSUser> CMSUsers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderCartItem> OrderCartItems { get; set; }
        public DbSet<OrderCart> OrderCarts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProductSpec> ProductSpecs { get; set; }
        public DbSet<ProductSpecOption> ProductSpecOptions { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductTypeSpec> ProductTypeSpecs { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<ProductSpecValue> ProductSpecValues { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<OrderCartItem>()
                .HasOne(b => b.Product)
                .WithMany(a => a.OrderCartItems)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductTag>()
                .HasOne(b => b.Product)
                .WithMany(a => a.ProductTags)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductCategory>()
               .HasOne(b => b.ParentCategory)
               .WithMany(a => a.ChildCategories)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
