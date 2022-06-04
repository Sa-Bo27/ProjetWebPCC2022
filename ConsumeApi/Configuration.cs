using System.Globalization;
using System.Net;
using ConsumeApi.Interface;
using ConsumeApi.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;

namespace ConsumeApi.Service
{
    public static class Configuration
    {
        public static string MovieBaseAdress { get; private set; }

        public static void UseServices(this IServiceCollection services)
        {
            // Handle both exceptions and return values in one policy
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
};
            var retryPolicy = Policy.Handle<HttpRequestException>()
                    .OrResult<HttpResponseMessage>(response => httpStatusCodesWorthRetrying.Contains(response.StatusCode))
                    .WaitAndRetryAsync(new[]{
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    });
            
            //Add the HttpClient and Polly for resilience

            services.AddHttpClient<IMovieServices, MovieService>()
                .AddPolicyHandler(retryPolicy)
                .AddTransientHttpErrorPolicy(policyBuilder => 
                    policyBuilder.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 2, 
                        durationOfBreak: TimeSpan.FromMinutes(1)
                        ));


        }


    }
}