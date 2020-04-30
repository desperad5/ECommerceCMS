using  SorubankCMS.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SorubankCMS.Data.Entity.Customer;

namespace  SorubankCMS.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CMSUser> CMSUsers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SorubankUser> SorubankUsers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<QuestionCard> QuestionCards { get; set; }
        public DbSet<TopicCard> TopicCards { get; set; }
        public DbSet<ExamCard> ExamCards { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BundleTopicCard> BundleTopicCards { get; set; }
        public DbSet<BundleQuestionCard> BundleQuestionCards { get; set; }
        public DbSet<BundleExamCard> BundleExamCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderCartItem> OrderCartItems { get; set; }
        public DbSet<OrderCart> OrderCarts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuestionCard>()
                .HasOne(b => b.Topic)
                .WithMany(a => a.QuestionCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<QuestionCard>()
                .HasOne(b => b.Lesson)
                .WithMany(a => a.QuestionCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TopicCard>()
                .HasOne(b => b.Topic)
                .WithMany(a => a.TopicCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TopicCard>()
                .HasOne(b => b.Lesson)
                .WithMany(a => a.TopicCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BundleExamCard>()
                .HasOne(b => b.Bundle)
                .WithMany(a => a.BundleExamCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BundleQuestionCard>()
                .HasOne(b => b.Bundle)
                .WithMany(a => a.BundleQuestionCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BundleTopicCard>()
                .HasOne(b => b.Bundle)
                .WithMany(a => a.BundleTopicCards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderCartItem>()
                .HasOne(b => b.Product)
                .WithMany(a => a.OrderCartItems)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductTag>()
                .HasOne(b => b.Product)
                .WithMany(a => a.ProductTags)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
