using Domain.Entities;
using Domain.ValueObjects;
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

            builder
                .OwnsOne(member => member.FirstName)
                    .Property(x => x.Value)
                    .HasMaxLength(FirstName.MaxLength);

            builder
                .OwnsOne(member => member.LastName)
                    .Property(x => x.Value)
                    .HasMaxLength(LastName.MaxLength); ;

            builder
                .OwnsOne(member => member.Email)
                    .Property(x => x.Value)
                    .IsRequired();
        }
    }
}
