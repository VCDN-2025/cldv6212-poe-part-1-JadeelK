using Azure.Storage.Blobs;

namespace Way2GoApp.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _container;

        public BlobStorageService(IConfiguration config)
        {
            var service = new BlobServiceClient(config.GetConnectionString("AzureStorage"));
            _container = service.GetBlobContainerClient("productimages");
            _container.CreateIfNotExists();
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var blob = _container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);
        }

        public List<string> ListFiles() =>
            _container.GetBlobs().Select(b => _container.Uri + "/" + b.Name).ToList();
    }
}