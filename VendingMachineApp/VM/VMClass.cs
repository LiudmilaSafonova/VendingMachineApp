using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

public enum Mode
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

        private decimal _ClientMoney = 0;
        private decimal _TotalMoney = 20m;

        public VendingMachine()
        {
            allProducts = new List<Product>();
            InitializeDefaultProducts();
        }

        private void InitializeDefaultProducts()
        {
            _mode = Mode.Admin;
            AddProduct("A1", "Cola", 12m, 10);
            AddProduct("B2", "Chips", 15m, 8);
            AddProduct("C3", "Water", 10m, 15);
            _mode = Mode.User;
        }

        public void AddProduct(string code, string name, decimal price, int quantity)
        {
            if (_mode != Mode.Admin)
            {
                Console.WriteLine("No access. Admin mode required");
                return;
            }

            Product product = new Product(code, name, price, quantity);
            allProducts.Add(product);
            Console.WriteLine($"Product '{name}' added successfully.");
        }

        public void SetMode(string code)
            {
            if (code == _AdminKey)
            {
                _mode = Mode.Admin;
                Console.WriteLine("User -> Admin");
            }
            else
                Console.WriteLine("Wrong code. Try again.");
            }

        public void SwitchMode2User()
        {
            if (_mode == Mode.Admin)
                _mode = Mode.User;
                Console.WriteLine("Admin -> User");
        }

        public Mode GetMode() => _mode;

        public void ShowAllProducts()
        {
            Console.WriteLine("List of all products:");
            Console.WriteLine("======================");
            Console.WriteLine("Code | Name\t| Price\t| Amount");
            Console.WriteLine("-----------------------------------");

            foreach (var product in allProducts)
            {
                Console.WriteLine($"[{product.ProductCode}] - {product.Name} ({product.Price} rub.) x {product.Amount}");
            }
        }

        public void FindProduct(string productCode)
        {
            var product = allProducts.FirstOrDefault(p => p.ProductCode == productCode);
            if (product != null)
            {
                Console.WriteLine($"Product found: {product.Name} - {product.Price} rub. (Available: {product.Amount})");
            }
            else
            {
                Console.WriteLine("Product not found.");
                return;
            }
        }

        public void AddAmountProduct(string productCode)
        {
            var product = allProducts.FirstOrDefault(p => p.ProductCode == productCode);
            if (product != null)
            {
                Console.WriteLine("Enter quantity of added products: ");
                int amount = int.Parse(Console.ReadLine());
                product.AddProduct(amount);
                Console.WriteLine($"Amount of {product.Name} is {product.Amount}");
            }
            else
            {
                Console.WriteLine($"Product with {productCode} code not found.");
            }
        }
        public void AddCoins(decimal value)
        {
            if (CoinIsValueNotAllowed(value))
            {
                Console.WriteLine("Invalid value. The coin must be 0.5, 1, 2, 5 or 10");
                return;
            }
            _ClientMoney += value;
        }
        private bool CoinIsValueNotAllowed(decimal value)
        {
            decimal[] allowedValues = { 0.5m, 1m, 2m, 5m, 10m };
            return !allowedValues.Contains(value);
        }
        public void Buy(string code)
        {
            var product = allProducts.FirstOrDefault(p => p.ProductCode == code);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Transaction t = new Transaction(product, _ClientMoney);
            t.Complete();

            if (t.Success)
            {

                _TotalMoney += product.Price;
                decimal change = t.Refund();
                if (change > 0)
                
                    Console.WriteLine($"Please take your change: {change} rub.");
                _ClientMoney = 0;
            }
        }
    }
}
