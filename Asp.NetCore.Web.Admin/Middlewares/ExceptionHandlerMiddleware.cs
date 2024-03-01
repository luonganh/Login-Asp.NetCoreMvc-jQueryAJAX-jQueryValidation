namespace Asp.NetCore.Web.Admin.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._request = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        private async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._request(context);
            }
            catch (Exception exception)
            {
                var exMess = $"Exception - {exception.Message}";
                var innerExMess = exception.InnerException != null ? $"InnerException - {exception.InnerException.Message}" : string.Empty;
                Log.Error(exception, "Request error: {0} ; {1}", exMess, innerExMess);
            }
        }
    }
}
