using Azure.Data.Tables;
using Way2GoApp.Models;

namespace Way2GoApp.Services
{
    public class TableStorageService
    {
        private readonly TableClient _customerTable;
        private readonly TableClient _productTable;

        public TableStorageService(IConfiguration config)
        {
            var serviceClient = new TableServiceClient(config.GetConnectionString("AzureStorage"));
            _customerTable = serviceClient.GetTableClient("Customers");
            _productTable = serviceClient.GetTableClient("Products");

            _customerTable.CreateIfNotExists();
            _productTable.CreateIfNotExists();
        }

        public async Task AddCustomer(Customer c) => await _customerTable.AddEntityAsync(c);
        public async Task<List<Customer>> GetCustomers() => _customerTable.Query<Customer>().ToList();

        public async Task AddProduct(Product p) => await _productTable.AddEntityAsync(p);
        public async Task<List<Product>> GetProducts() => _productTable.Query<Product>().ToList();
    }
}