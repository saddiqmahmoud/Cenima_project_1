using Cenima_project.NewModels;
using Microsoft.EntityFrameworkCore;





namespace Cenima_project.Data
{
    public class ApplicationdbContext : DbContext
    {

        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog =MyData;Integrated Security=True; Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }


    }
}
