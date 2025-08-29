using Microsoft.AspNetCore.Mvc;
using Way2GoApp.Models;
using Way2GoApp.Services;

namespace Way2GoApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly TableStorageService _tableService;
        private readonly BlobStorageService _blobService;

        public ProductController(TableStorageService tableService, BlobStorageService blobService)
        {
            _tableService = tableService;
            _blobService = blobService;
        }

        // Show all products + images
        public async Task<IActionResult> Index()
        {
            var products = await Task.FromResult(_tableService.GetProducts());
            ViewBag.Images = _blobService.ListFiles();
            return View(products);
        }

        // Show form
        public IActionResult Create() => View();

        // Save new product
        [HttpPost]
        public async Task<IActionResult> Create(Product p, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                await _tableService.AddProduct(p);
                if (imageFile != null)
                    await _blobService.UploadFileAsync(imageFile);
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}