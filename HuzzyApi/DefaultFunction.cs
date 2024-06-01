using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HuzzyApi
{
    public class DefaultFunction
    {
        private readonly ILogger<DefaultFunction> _logger;

        public DefaultFunction(ILogger<DefaultFunction> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            await Task.Delay(5000);
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
