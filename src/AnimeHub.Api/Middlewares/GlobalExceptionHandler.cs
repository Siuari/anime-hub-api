using AnimeHub.Domain.DomainExceptions;

namespace AnimeHub.Api.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AnimeHubValidationException ex)
            {
                _logger.LogWarning(ex, "Ocorreram erros de validação.");
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro interno do servidor.");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = "Ocorreu um erro interno do servidor. Por favor, tente novamente"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
