using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum Mode
{ 
    User,
    Admin
}

namespace VendingMachineApp.VendingMachineApp.VM
{
    public class VendingMachine
    {
        private const string _AdminKey = "12345";
        private Mode _mode = Mode.User;

        private List<Product> allProducts;

        public VendingMachine()
        {
            allProducts = new List<Product>();
            InitializeDefaultProducts();
        }

        private void InitializeDefaultProducts()
        {
            AddProduct("Cola", 89.99m, 10, "A1");
            AddProduct("Chips", 129.50m, 8, "B2");
            AddProduct("Water", 50.00m, 15, "C3");
        }

        public void AddProduct(string name, decimal price, int quantity, string code)
        {
            if (_mode != Mode.Admin)
            {
                Console.WriteLine("No access. Admin mode required");
                return;
            }

            Product product = new Product(name, price, quantity, code);
            allProducts.Add(product);
            Console.WriteLine($"Product '{name}' added successfully.");
        }

        public void SetMode(string code)
            {
                if (code == _AdminKey)
                    _mode = Mode.Admin;
                else 
                    Console.WriteLine("Wrong code. Try again.");
            }

        public void GetMode() => Console.WriteLine(_mode);

        public void ShowAllProducts()
        {
            Console.WriteLine("Список всех продуктов:");
            Console.WriteLine("======================");
            Console.WriteLine("Code | Name\t| Price\t| Amount");
            Console.WriteLine("-----------------------------------");

            foreach (var product in allProducts)
            {
                Console.WriteLine($"{product.GetProductCode()}\t| {product.GetName()}\t| {product.GetPrice()} руб.\t| {product.GetAmount()}");
            }
        }
    }
}
