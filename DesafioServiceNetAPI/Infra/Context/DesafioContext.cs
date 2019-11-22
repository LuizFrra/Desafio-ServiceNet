﻿using DesafioServiceNetAPI.Infra.EntityConfig;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("tbl_users");
            builder.Entity<Client>().ToTable("tbl_clients");

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new ClientConfig());

            builder.Entity<Client>().HasOne(u => u.User).WithMany(c => c.Clients).IsRequired().HasForeignKey(u => u.UserID);
        }
    }
}
