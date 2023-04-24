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

        public void Seed()
        {
            var photoSeed = new Photo[]
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

                Photos.AddRange(photoSeed);

            
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

            if (Users.Any(u => u.Email == "admin@dev.com" || u.Email == "user@dev.com") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");
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
