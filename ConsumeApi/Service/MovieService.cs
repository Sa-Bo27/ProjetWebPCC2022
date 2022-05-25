using ConsumeApi.Interface;
using ConsumeApi.Model;
using ConsumeApi.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ConsumeApi
{
    public class MovieService : IMovieServices
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<MovieUrl> _options;

        public MovieService(HttpClient httpClient, IOptions<MovieUrl> options)
        {
            _options = options;

            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);


        }


        public async Task<ListMovies> GetAllMoviesByName(ParametresQuery parametresQuery)
        {

            var httpResponse = await _httpClient.GetAsync($"search/movie?api_key={_options.Value.ApiKey}&language=en-US&query={parametresQuery.MovieToFind}&page={parametresQuery.Page}&include_adult={parametresQuery.IncludeAdulte}");

            if (!httpResponse.IsSuccessStatusCode) throw new Exception("Cannot retrive task in service");

            var content = await httpResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<ListMovies>(content);
            return movies;
        }

        public async Task<ListMovies> GetPopularMovies(ParametresQuery parametresQuery){

            var httpResponse = await _httpClient.GetAsync($"movie/popular?api_key={_options.Value.ApiKey}&language=en-US&page={parametresQuery.Page}");

            if(!httpResponse.IsSuccessStatusCode) throw new Exception("Cannot retrive task in service");

            var content = await httpResponse.Content.ReadAsStringAsync();
            var popularMovies = JsonConvert.DeserializeObject<ListMovies>(content);

            return popularMovies;
        }

        
    }
}