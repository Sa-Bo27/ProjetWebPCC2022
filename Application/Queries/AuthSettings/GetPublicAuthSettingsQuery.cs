using Application.Common.Exceptions;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Application.Queries.AuthSettings
{
    public class GetPublicAuthSettingsQuery: IRequest<AuthSettingsModel>
    {
        
    }

    public class GetPublicAuthSettingsQueryHandler : IRequestHandler<GetPublicAuthSettingsQuery, AuthSettingsModel>
    {
        private  IConfiguration _configuration { get; }
            public GetPublicAuthSettingsQueryHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

        public async Task<AuthSettingsModel> Handle(GetPublicAuthSettingsQuery request, CancellationToken cancellationToken)
        {
            
            try{

                var dto = new AuthSettingsModel(){

                    Audience = _configuration.GetValue<string>("Auth0:Audience"),
                    Domain = _configuration.GetValue<string>("Auth0:Domain"),
                    ClientID = _configuration.GetValue<string>("Auth0:ClientID")
                };
                return dto;

            }catch(Exception){

                throw new NotFoundException(nameof(AuthSettingsModel), request);
            }
        }
    }
}