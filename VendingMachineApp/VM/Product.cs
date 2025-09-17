using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.VendingMachineApp.VM
{
    public class Product
    {
        private string Name { get; set; }
        private decimal Price { get; set; }
        private int Amount { get; set; }
        private string ProductCode { get; set; }
        public Product(string name, decimal price, int amount, string code)
        {
            Name = name;
            Price = price;
            Amount = amount;
            ProductCode = code;
        }

        public string GetName() => Name;
        public decimal GetPrice() => Price;
        public int GetAmount() => Amount;
        public string GetProductCode() => ProductCode;
        public bool Dispense()
        {
            if (Amount > 0)
            {
                Amount--;
                Console.WriteLine("Product issued.");
                return true;
            }
            Console.WriteLine("Quatity of product is 0.");
            return false;
        }
        public void AddProduct (int amount) => Amount += amount;
    }
}
