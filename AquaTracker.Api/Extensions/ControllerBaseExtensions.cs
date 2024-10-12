using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace AquaTracker.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ActionResult ToActionResult<T, TResponse>(
            this ControllerBase controller,
            ErrorOr<T> result,
            Func<T, TResponse> successMap) where TResponse : ActionResult
        {
            if (!result.IsError)
            {
                var response = successMap(result.Value);
                return response;
            }

            var firstError = result.Errors.First();
            var statusCode = GetStatusCodeForError(firstError);

            return controller.Problem(
                statusCode: statusCode,
                title: GetTitleForError(firstError),
                detail: string.Join(",", result.Errors.Select(e => e.Description)));
        }

        private static int GetStatusCodeForError(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        private static string GetTitleForError(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => "Validation error occurred",
                ErrorType.NotFound => "Resource not found",
                ErrorType.Conflict => "Conflict error",
                _ => "An unexpected error occurred"
            };
        }
    }
}