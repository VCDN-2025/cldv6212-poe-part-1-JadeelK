using Microsoft.AspNetCore.Mvc;
using Way2GoApp.Models;
using Way2GoApp.Services;

namespace Way2GoApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly QueueService _queueService;
        public OrderController(QueueService queueService) { _queueService = queueService; }

        // Show orders (messages)
        public async Task<IActionResult> Index()
        {
            var msgs = await _queueService.GetMessages();
            return View(msgs);
        }

        // Show form
        public IActionResult Create() => View();

        // Add new order message
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                string msg = $"Processing order {order.OrderId} for {order.CustomerName}, product: {order.ProductName}, qty: {order.Quantity}";
                await _queueService.AddMessage(msg);
                return RedirectToAction("Index");
            }
            return View(order);
        }
    }
}