using ConsumeApi.Model;

namespace ConsumeApi.Interface
{
    public interface IMovieServices
    {
             
    
         Task<ListMovies> GetAllMoviesByName(ParametresQuery parametresQuery);
         Task<ListMovies> GetPopularMovies(ParametresQuery parametresQuery);
    
    }
}