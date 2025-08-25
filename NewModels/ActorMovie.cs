namespace Cenima_project.NewModels
{
    public class ActorMovie
    {

        public int Id { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }


        public int MoviesId { get; set; }
        public Movie Movie { get; set; }
    }
}
