﻿namespace Cenima_project.NewModels
{
    public class Cinema
    {

        public int Id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string CinemaLogo { get; set; }
        public string Address { get; set; }
        public List<Movie> Movie { get; set; }

    }
}
