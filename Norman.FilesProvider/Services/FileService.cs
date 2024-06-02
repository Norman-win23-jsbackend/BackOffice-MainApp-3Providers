using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Norman.Common.Services.Models;

namespace Norman.FilesProvider.Services
{
    public class FileService
    {
        private readonly DataContext context;
        private readonly ILogger<FileService> logger;
        private readonly BlobServiceClient client;
        private BlobContainerClient? container;
        public FileService(DataContext context, ILogger<FileService> logger, BlobServiceClient client)
        {
            this.context = context;
            this.logger = logger;
            this.client = client;
        }
        public string SetFileName(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            return fileName;
        }

        public async Task SetBlobContainerAsync(string containerName)
        {
            var container = client.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync();
        }
        public async Task<string> UploadFileAsync(IFormFile file, Files fileEntity)
        {
            BlobHttpHeaders headers = new()
            {
                ContentType = file.ContentType
            };

            var blobClient = container!.GetBlobClient(fileEntity.FileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, headers);

            return blobClient.Uri.ToString();
        }
        public async Task SaveToDatabaseAsync(Files fileEntity)
        {
            context.Files.Add(fileEntity);
            await context.SaveChangesAsync();
        }
    }
}
