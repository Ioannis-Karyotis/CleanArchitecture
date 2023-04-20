using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(member => member.Id);

            builder.Property(member => member.FirstName).HasMaxLength(100);

            builder.Property(member => member.LastName).HasMaxLength(100);

            builder.Property(member => member.Email).IsRequired();
        }
    }
}
