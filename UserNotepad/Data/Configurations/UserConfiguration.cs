using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar")
                .HasMaxLength(6);

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(19);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(150);
        }
    }
}
