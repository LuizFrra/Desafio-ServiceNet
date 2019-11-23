using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Infra.EntityConfig
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(k => k.ClientID);
            builder.HasIndex(n => n.Name).IsUnique(false);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired(true);
            builder.Property(p => p.NumberAddress).HasMaxLength(10).IsRequired(true);
            builder.Property(p => p.Country).HasMaxLength(50).IsRequired(true);
            builder.Property(p => p.Address).HasMaxLength(70).IsRequired(true);
            builder.Property(p => p.CepId).IsRequired(true);
        }
    }
}
