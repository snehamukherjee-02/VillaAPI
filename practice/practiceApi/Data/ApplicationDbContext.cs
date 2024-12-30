using Microsoft.EntityFrameworkCore;
using practiceApi.Models;

namespace practiceApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Villa> VillaTbl { get; set; }
        public DbSet<Comment> CommentsTbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().Property(u => u.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Villa>().Property(u => u.CreatedDate).HasDefaultValueSql("getdate()");
            //modelBuilder.Entity<Villa>().Property(u => u.UpdatedDate).HasColumnType("datetime2").HasDefaultValue(null);
            modelBuilder.Entity<Villa>().Property(u => u.UpdatedDate).HasColumnType("datetime2").IsRequired(false);

            modelBuilder.Entity<Comment>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comment>().Property(u => u.CreatedOn).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = Guid.NewGuid(),
                    Name = "Royal Villa",
                    Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Rate = "200",
                    Sqft = 550,
                    Occupency = 5,
                    Imageurl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    Amenity = "",
                    
                },
                 new Villa()
                 {
                     Id = Guid.NewGuid(),
                     Name = "Premium Pool Villa",
                     Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                     Rate = "300",
                     Sqft = 550,
                     Occupency = 5,
                     Imageurl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                     Amenity = "",
                     
                 },
                  new Villa()
                  {
                      Id = Guid.NewGuid(),
                      Name = "Luxury Pool Villa",
                      Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                      Rate = "400",
                      Sqft = 450,
                      Occupency = 5,
                      Imageurl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                      Amenity = "",
                  
                  },
                   new Villa()
                   {
                       Id = Guid.NewGuid(),
                       Name = "Diamond Villa",
                       Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                       Rate = "500",
                       Sqft = 750,
                       Occupency = 5,
                       Imageurl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                       Amenity = "",
                       
                   },
                    new Villa()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Diamond Pool Villa",
                        Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        Rate = "900",
                        Sqft = 950,
                        Occupency = 10,
                        Imageurl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                        Amenity = "",
                        
                    }
            );

          
        }
    }
}
