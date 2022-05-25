using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace API.Filters
{
    //Filtre qui se lance apres une exception//
    public class OnExceptionFilter : IExceptionFilter 
    {
        public void OnException(ExceptionContext context)
        {
            //envoyer le context dans serilog
            Log.Error("An exception has occured... {exception} - {stackTrace} ", context.Exception.Message, context.Exception.StackTrace);
        }
    }
}