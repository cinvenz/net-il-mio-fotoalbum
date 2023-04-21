using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext :DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PhotoDb;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
        }

        public void Seed()
        {
            if (!Photos.Any())
            {
                var seed = new Photo[]
                {
            new Photo
            {
                Title = "Colosseo",
                Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                Image = "https://picsum.photos/id/237/200/300",
                Visible = true,
            },
             new Photo
            {
                Title = "Gioconda",
                Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                Image = "https://picsum.photos/id/237/200/300",
                Visible = true,
            },
              new Photo
            {
                Title = "La statua della Libertà",
                Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                Image = "https://picsum.photos/id/237/200/300",
                Visible = true,
            },
               new Photo
            {
                Title = "Cristo Redentore",
                Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                Image = "https://picsum.photos/id/237/200/300",
                Visible = true,
            },
                };

                Photos.AddRange(seed);

                SaveChanges();  
            }
        }

    }
}
