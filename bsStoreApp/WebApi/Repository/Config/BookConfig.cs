using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Repository.Config
{
    public class BookConfig : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.HasData(
                new Books { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Books { Id = 2, Title = "Mesnevi", Price = 175 },
                new Books { Id = 3, Title = "Devlet", Price = 375 }
                );
        }
    }
}
