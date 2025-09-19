using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

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
            allProducts.Add(new Product("B2", "Chips", 15m, 8));
            allProducts.Add(new Product("C3", "Water", 10m, 15));
            allProducts.Add(new Product("A1", "Cola", 12m, 10));
        }

        public void AddProduct()
        {
            if (_mode != Mode.Admin)
            {
                Console.WriteLine("No access. Admin mode required");
                return;
            }
            Console.WriteLine("Add new product");
            Console.Write("Code: ");
            string code = Console.ReadLine();     //TODO: check if the code isn't used
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Cost: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Amount: ");
            int quantity = int.Parse(Console.ReadLine());


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
                Console.WriteLine("Wrong code.");
            }

        public void SwitchMode2User()
        {
            if (_mode == Mode.Admin)
            {
                _mode = Mode.User;
                Console.WriteLine("Admin -> User");
                return;
            }
            Console.WriteLine("No access.");
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

        public void AddAmountProduct()
        {
            if(_mode != Mode.Admin)
            {
                Console.WriteLine("No access. Admin mode required");
                return;
            }
            string productCode = Console.ReadLine();
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
                Console.WriteLine("Invalid value. The coin must be 1, 2, 5 or 10. Paper money in 10, 50, 100 and 500");
                return;
            }
            _ClientMoney += value;
        }
        private bool CoinIsValueNotAllowed(decimal value)
        {
            decimal[] allowedValues = { 1m, 2m, 5m, 10m, 50m, 100m, 500m };
            return !allowedValues.Contains(value);
        }
        public void ReturnMoney2User()
        {
            if (_ClientMoney == 0)
            {
                Console.Write("No money to return ");
                return;
            }
            Console.WriteLine($"Take your money back: { _ClientMoney}");
            _ClientMoney = 0;
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
