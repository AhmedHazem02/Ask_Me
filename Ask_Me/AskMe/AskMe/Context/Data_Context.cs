using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMe.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace AskMe.Context
{
    public class Data_Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=Ask_Me;trusted_connection=true");
        }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Question> Question { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(Q=>Q.ToUser)
                .WithMany(T => T.Question_To)
                .HasForeignKey(K=>K.To_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(Q=>Q.FromUser)
                .WithMany(F=>F.Question_From)
                .HasForeignKey(K=>K.From_ID)
                .OnDelete(DeleteBehavior.Restrict);

             

            base.OnModelCreating(modelBuilder);
        }


    }
}
