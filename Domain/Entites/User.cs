using System.Text.Json.Serialization;

namespace Domain.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        
        public List<MovieDom>? ListMovies { get; set; } = new();

        public void AddMovie(MovieDom movie)
        {
            if (ListMovies.All(item => item.Title != movie.Title))
            {
                ListMovies.Add(new MovieDom
                {
                    Adult = movie.Adult,
                    Backdrop_path = movie.Backdrop_path,
                    Original_language = movie.Original_language,
                    Original_title = movie.Original_title,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    Poster_path = movie.Poster_path,
                    Release_date = movie.Release_date,
                    Title = movie.Title
                });
            }

            var existingItem = ListMovies.FirstOrDefault(item => item.Title == movie.Title);
            if (existingItem != null) existingItem = movie;
        }

        public void RemoveMovie(int movieId)
        {
            var movie = ListMovies.FirstOrDefault(item => item.Id == movieId);
            if (movie == null) return;
            
            ListMovies.Remove(movie);
        }

    }
}