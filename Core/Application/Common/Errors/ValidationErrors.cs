using Architecture.Application.Common.Models;
using System.Net;

namespace Architecture.Application.Common.Errors
{
    public static class ValidationErrors
    {
        public static Error FluentValidationErrors(Dictionary<string, string> validationErrors) => new(HttpStatusCode.BadRequest, "Validation Error", validationErrors);
        public static Error ItemNotFound(string ItemId) => new(HttpStatusCode.NotFound, $"Item Not Found {ItemId}");
    }
}
