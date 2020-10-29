using digitalsign.domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace digitalsign.persistence.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.CreatedUser)
                .WithMany(u => u.CreatedTasks)
                .IsRequired(true);
            builder.HasOne(t => t.CompletedUser)
                .WithMany(u => u.CompletedTasks)
                .IsRequired(false);
        }
    }
}
