using Microsoft.AspNetCore.Mvc;
using Way2GoApp.Services;

namespace Way2GoApp.Controllers
{
    public class ContractController : Controller
    {
        private readonly FileShareService _fileService;
        public ContractController(FileShareService fileService) { _fileService = fileService; }

        // List contracts
        public IActionResult Index()
        {
            var files = _fileService.ListFiles();
            return View(files);
        }

        // Show upload form
        public IActionResult Upload() => View();

        // Upload new contract
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
                await _fileService.UploadFileAsync(file);

            return RedirectToAction("Index");
        }
    }
}