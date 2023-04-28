using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>

    {
		
		public PhotoContext(DbContextOptions<PhotoContext> options) : base(options) { }

		public DbSet<Photo> Photos { get; set; }
		public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }

        public void Seed()
        {
           
                var photoSeed = new Photo[]
                {
                new Photo
                {
                    Title = "Colosseo",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                    Image = "https://www.turismoroma.it/sites/default/files/colosseo_slide_0.jpg",
                    Visible = true,
                },
                    new Photo
                {
                    Title = "Gioconda",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                    Image = "https://media-assets.ad-italia.it/photos/62961fb73236e8dd2aa42f9c/16:9/w_2992,h_1683,c_limit/GettyImages-1174275340.jpg",
                    Visible = true,
                },
                    new Photo
                {
                    Title = "La statua della Libertà",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                    Image = "https://media-assets.vanityfair.it/photos/614c9482e6e64c0e0b190706/16:9/pass/Statua-della-Libert%C3%A0-Portrait.jpeg",
                    Visible = true,
                },
                    new Photo
                {
                    Title = "Cristo Redentore",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit.",
                    Image = "https://media.vaticannews.va/media/content/dam-archive/vaticannews/multimedia/2021/10/11/AdobeStock_109601187AEM.jpg/_jcr_content/renditions/cq5dam.thumbnail.cropped.750.422.jpeg",
                    Visible = true,
                },
                };
                if (!Photos.Any())
                {
                    Photos.AddRange(photoSeed);
                }
            
            if (!Categories.Any())
            {
                var seed = new Category[]
                {
                    new()
                    {
                        Title = "travel"
					},
                    new()
                    {
                        Title = "beautiful",
                        Photos = photoSeed
					},
                    new()
                    {
                        Title = "instagram"
					},

					 new()
					{
						Title = "nature"
					}
				};
				Categories.AddRange(seed);
            }

            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("Admin"),
                    new("User")
                };

                Roles.AddRange(seed);
            }

            if (Users.Any(u => u.Email == "vincenzo@dev.com" || u.Email == "user@dev.com") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "vincenzo@dev.com");
                var user = Users.First(u => u.Email == "user@dev.com");

                var adminRole = Roles.First(r => r.Name == "Admin");
                var userRole = Roles.First(r => r.Name == "User");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                    },
                    new()
                    {
                    UserId = user.Id,
                    RoleId = userRole.Id
                    },
                };

                UserRoles.AddRange(seed);
            }
            SaveChanges();

		}

	}
}
