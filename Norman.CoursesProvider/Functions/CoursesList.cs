using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Norman.Common.Services.Interfaces;
using Norman.Common.Services.Models;

namespace Nour.Courses.Functions
{
    public class CoursesList
    {
        private readonly ILogger<CoursesList> _logger;
        private readonly ICosmosDBInterface cosmosDBInterface;
        private readonly IConfiguration configuration;

        public CoursesList(ILogger<CoursesList> logger, ICosmosDBInterface cosmosDBInterface, IConfiguration configuration)
        {
            _logger = logger;
            this.cosmosDBInterface = cosmosDBInterface;
            this.configuration = configuration;
        }

        [Function("CoursesList")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            var response = await cosmosDBInterface.GetAllAsync<Norman.Common.Services.Models.Courses>(configuration["CosmosDbName"], configuration["CosmosContainerCourses"]);
            return new OkObjectResult(response);
        }
    }
}
