using Azure.Storage.Files.Shares;

namespace Way2GoApp.Services
{
    public class FileShareService
    {
        private readonly ShareClient _share;

        public FileShareService(IConfiguration config)
        {
            var service = new ShareServiceClient(config.GetConnectionString("AzureStorage"));
            _share = service.GetShareClient("contracts");
            _share.CreateIfNotExists();
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var dir = _share.GetRootDirectoryClient();
            var fileClient = dir.GetFileClient(file.FileName);
            using var stream = file.OpenReadStream();
            await fileClient.CreateAsync(stream.Length);
            await fileClient.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
        }

        public List<string> ListFiles()
        {
            var dir = _share.GetRootDirectoryClient();
            return dir.GetFilesAndDirectories()
                      .Where(f => !f.IsDirectory)
                      .Select(f => f.Name).ToList();
        }
    }
}
