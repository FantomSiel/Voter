using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Voter.Dal.Models;

namespace Voter.Dal
{
    public class VoteDbContext : DbContext
    {
        public VoteDbContext(DbContextOptions<VoteDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<AnswerAggregate>(
                x =>
                {
                    x.Property<Guid>("QuestionId");
                    x.HasOne<Question>().WithMany().HasForeignKey("QuestionId");
                });

            builder.Entity<AnswerAggregate>(
                x =>
                {
                    x.Property<Guid>("VariantId");
                    x.HasOne<Variant>().WithMany().HasForeignKey("VariantId");
                });
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<AnswerAggregate> AnswerAggregates { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
