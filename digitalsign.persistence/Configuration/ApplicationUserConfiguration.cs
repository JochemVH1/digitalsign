using System;
using digitalsign.persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace digitalsign.persistence.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(u => u.RefreshToken)
                .WithOne(r => r.User);

            builder.ToTable("Users");
        }
    }
}
