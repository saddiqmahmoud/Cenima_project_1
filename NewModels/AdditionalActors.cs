namespace Cenima_project.NewModels
{
    public class AdditionalActors
    {
        public Movie Movies { get; set; }
        public List<ActorMovie> ActorMovies { get; set; }
        public Actor Actor { get; set; }

    }
}
