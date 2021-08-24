using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ex9_ORM_EFCore_Console.Models;
using ex9_ORM_EFCore_Console;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ex9_ORM_EFCore_Console.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            
        }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientInfo> ClientInfos { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.Configuration.GetConnectionString("Database"));
            optionsBuilder.LogTo(msg => System.Diagnostics.Debug.WriteLine(msg), 
                Microsoft.Extensions.Logging.LogLevel.Information); // log to debugger
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // model configuration
            modelBuilder.Entity<Client>(ClientConfigure);
            modelBuilder.Entity<Card>(CardConfigure);
            modelBuilder.Entity<Bank>(BankConfigure);


            // configuration of relations
            
            // one to one Client and ClientInfo
            modelBuilder.Entity<Client>()
                .HasOne(client => client.ClientInfo)
                .WithOne(info => info.Client)
                .HasForeignKey<ClientInfo>(info => info.ClientId);

            // one to many Client and Cards
            modelBuilder.Entity<Client>()
                .HasMany(client => client.Cards)
                .WithOne(card => card.Client)
                .HasForeignKey(card => card.ClientId);

            // many to many Client and Bank
            modelBuilder.Entity<Client>()
                .HasMany(client => client.Banks)
                .WithMany(bank => bank.Clients);
                //.UsingEntity(t => t.ToTable("ClientsBanks"));
                /*
                 * По замовчуванню створилася таблиця BankClient, що можна побачити у 
                 * міграції AddMoreRelationForClientModel
                 * **/

        }

        private void ClientConfigure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasAlternateKey(client => client.UserName);

            builder
                .Property(client => client.UserName)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(client => client.Id)
                .HasColumnName("client_id");
        }

        private void CardConfigure(EntityTypeBuilder<Card> builder)
        {
            builder
                .HasAlternateKey(card => card.Number);

            builder
                .Property(card => card.Number)
                .HasColumnName("card_number");
        }

        private void BankConfigure(EntityTypeBuilder<Bank> builder)
        {
            builder
                .HasAlternateKey(bank => bank.Name);
        }

    }
}
