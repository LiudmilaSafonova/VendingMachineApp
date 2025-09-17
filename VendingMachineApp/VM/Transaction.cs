using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.VendingMachineApp.VM
{
    public class Transaction
    {
        public Product Product { get; private set; }
        public decimal AmountPaid { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool Success { get; private set; }

        public Transaction(Product product, decimal amountPaid)
        {
            Product = product;
            AmountPaid = amountPaid;
            Timestamp = DateTime.Now;
            Success = false;
        }

        public void Complete()
        {
            if (AmountPaid < Product.Price)
            {
                Console.WriteLine("Not enough money. You can add more or take money back.");
                return;
            }
            else if (AmountPaid >= Product.Price)
            {
                Success = true;
                Product.Dispense();
                Console.WriteLine($"Transaction successful! {Product.Name} dispensed.");
            }
            else
            {
                Console.WriteLine("Transaction failed.");
                return;
            }
        }

        public decimal Refund()
        {
            if (!Success) return AmountPaid;
            return AmountPaid - Product.Price;
        }
    }
}
