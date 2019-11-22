using DesafioServiceNetAPI.Infra.EntityConfig;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Infra.Context
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CEP> CEP { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("tbl_users");
            builder.Entity<Client>().ToTable("tbl_clients");
            builder.Entity<CEP>().ToTable("tbl_ceps");

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new ClientConfig());
            builder.ApplyConfiguration(new CepConfig());

            builder.Entity<Client>().HasOne(u => u.User).WithMany(c => c.Clients).IsRequired().HasForeignKey(u => u.UserID);
            builder.Entity<Client>().HasOne(c => c.Cep).WithMany(c => c.Clients).IsRequired().HasForeignKey(c => c.CepId);
        }
    }
}
