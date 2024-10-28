using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfigurations : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder
            .HasKey(e => e.Id);

            builder
                .HasOne(c => c.Project)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
