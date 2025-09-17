using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.VendingMachineApp.VM
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Amount { get; private set; }
        public string ProductCode { get; private set; }
        public Product(string code, string name, decimal price, int amount)
        {
            Name = name;
            Price = price;
            Amount = amount;
            ProductCode = code;
        }

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
