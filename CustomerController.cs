using Microsoft.AspNetCore.Mvc;
using Way2GoApp.Models;
using Way2GoApp.Services;

namespace Way2GoApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TableStorageService _service;
        public CustomerController(TableStorageService service) { _service = service; }

        // Show all customers
        public async Task<IActionResult> Index()
        {
            var customers = await Task.FromResult(_service.GetCustomers());
            return View(customers);
        }

        // Show form
        public IActionResult Create() => View();

        // Save new customer
        [HttpPost]
        public async Task<IActionResult> Create(Customer c)
        {
            if (ModelState.IsValid)
            {
                await _service.AddCustomer(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }
    }
}
