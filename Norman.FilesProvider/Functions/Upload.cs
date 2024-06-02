using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Norman.Common.Services.Models;
using Norman.FilesProvider.Services;

namespace Norman.Common.Services.Functions
{
    public class Upload
    {
        private readonly ILogger<Upload> _logger;
        private readonly FileService fileService;

        public Upload(ILogger<Upload> logger, FileService fileService)
        {
            _logger = logger;
            this.fileService = fileService;
        }

        [Function("Upload")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequest req)
        {

            try
            {
                if (req.Form.Files["file"] is IFormFile file)
                {
                    var containerName = !string.IsNullOrEmpty(req.Query["containerName"]) ? req.Query["containerName"].ToString() : "files";
                    var fileEntity = new Files
                    {
                        FileName = fileService.SetFileName(file),
                        ContentType = file.ContentType,
                        ContainerName = containerName
                    };

                    await fileService.SetBlobContainerAsync(fileEntity.ContainerName);
                    var filePath = await fileService.UploadFileAsync(file, fileEntity);
                    fileEntity.FilePath = filePath;

                    await fileService.SaveToDatabaseAsync(fileEntity);
                    return new OkObjectResult(fileEntity);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new BadRequestResult();
        }
    }
}
