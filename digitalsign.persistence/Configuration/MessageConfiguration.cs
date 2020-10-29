using digitalsign.domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace digitalsign.persistence.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Guid);
            builder.HasOne(m => m.FromUser)
                .WithMany(u => u.Messages);
            builder.HasOne(m => m.Task)
                .WithOne(t => t.Message)
                .HasForeignKey<Task>(t => t.MessageId)
                .IsRequired(false);
        }
    }
}
