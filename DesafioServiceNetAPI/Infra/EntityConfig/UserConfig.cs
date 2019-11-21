using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioServiceNetAPI.Infra.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.UserID);
            builder.HasIndex(p => p.Email).IsUnique(true);
            builder.Property(p => p.Name).IsRequired(true);
            builder.Property(p => p.Password).IsRequired(true);
            builder.Property(p => p.LastUpdate).HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.LastUpdate).IsConcurrencyToken(true);
            builder.Property(p => p.Name).HasMaxLength(70);
            builder.Property(p => p.Email).HasMaxLength(50);
        }
    }
}
