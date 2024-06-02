using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Norman.Common.Services.Interfaces;

namespace Nour.Courses.Functions
{
    public class OneCourse
    {
        private readonly ILogger<OneCourse> _logger;
        private readonly ICosmosDBInterface cosmosDBInterface;
        private readonly IConfiguration configuration;

        public OneCourse(ILogger<OneCourse> logger, ICosmosDBInterface cosmosDBInterface, IConfiguration configuration)
        {
            _logger = logger;
            this.cosmosDBInterface = cosmosDBInterface;
            this.configuration = configuration;
        }

        [Function("OneCourse")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            string body = null!;
            try
            {
                body = await new StreamReader(req.Body).ReadToEndAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"StreamReader :: {ex.Message}");
            }
            if (body != null)
            {
                string dbName = configuration["Values:CosmosDbName"];
                string containerName = configuration["Values:CosmosContainerCourses"];
                var id = JsonConvert.DeserializeObject<string>(body)!;
                var response = await cosmosDBInterface.GetOneAsync<Norman.Common.Services.Models.Courses>(dbName, containerName, id);
                return new OkObjectResult(response);
            }

            return new BadRequestResult();
        }
    }
}
