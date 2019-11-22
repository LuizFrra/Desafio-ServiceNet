using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioServiceNetAPI.Infra.EntityConfig
{
    public class CepConfig : IEntityTypeConfiguration<CEP>
    {
        public void Configure(EntityTypeBuilder<CEP> builder)
        {
            builder.HasKey(k => k.CepID);
            builder.Property(p => p.CepID).ValueGeneratedNever();
            builder.Property(p => p.State).IsRequired(true).HasMaxLength(20);
            builder.Property(p => p.City).IsRequired(true).HasMaxLength(50);
        }
    }
}
