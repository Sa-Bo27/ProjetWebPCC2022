using FluentValidation.Results;

namespace Application.Common.Behaviours
{
    public class ValidationException: Exception
    {
        public ValidationException(): base("One more validation failures have occured..")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures): this()
        {
            Errors = failures
                .GroupBy(e=>e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        public IDictionary<string, string[]> Errors { get;  }
    }
}