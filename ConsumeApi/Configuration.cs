using System.Globalization;
using System.Net;
using ConsumeApi.Interface;
using ConsumeApi.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;



namespace ConsumeApi.Service
{
    public static class Configuration
    {
        public static string MovieBaseAdress {get; private set;}
        
        public static void UseServices(this IServiceCollection services)
        {
            
            services.AddHttpClient<IMovieServices, MovieService>();

        }
    }
}