using VendingMachineApp.VendingMachineApp.VM;

class Program
{
    static void Main(string[] args)
    {
        VendingMachine vm = new VendingMachine();

        while (true)
        {
            if (vm.GetMode() == Mode.User)
            {
                Console.WriteLine("\n1 - Show products");
                Console.WriteLine("2 - Insert coins[add paper moneyy]");
                Console.WriteLine("3 - Buy product [wait for answer]");
                Console.WriteLine("4 - Take money back [not ready]");
                Console.WriteLine("5 - Enter admin mode");
                Console.WriteLine("q - Quit");

            }
            else
            {
                Console.WriteLine("101 - add quantity of product");
                Console.WriteLine("102 - add new product [not ready, check for unique code]");
                Console.WriteLine("103 - take money [not ready]");
                Console.WriteLine("l - log out");
                Console.WriteLine("q - Quit");
            }

            Console.Write("Choose action: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    vm.ShowAllProducts();
                    break;
                case "2":
                    Console.Write("Enter summ: ");
                    decimal money = decimal.Parse(Console.ReadLine());
                    vm.AddCoins(money);
                    break;
                case "3":
                    Console.Write("Please enter the desired item code: ");
                    string code = Console.ReadLine();
                    vm.Buy(code);
                    break;
                case "4":
                    Console.Write("your monet bak");
                    break;
                case "5":
                    Console.Write("Enter admin code: ");
                    string key = Console.ReadLine();
                    vm.SetMode(key);
                    break;
                case "q":
                    return;
                case "101":
                    Console.Write("Enter produt code: ");
                    string prod_code = Console.ReadLine();
                    vm.FindProduct(prod_code);
                    vm.AddAmountProduct(prod_code);
                    break;
                case "102":
                    Console.Write("Add new product");
                    Console.Write("Code: ");
                    string p_code = Console.ReadLine();     //TODO: check if the code isn't used
                    Console.Write("Name: ");
                    string p_name = Console.ReadLine();
                    Console.Write("Cost: ");
                    decimal p_cost = decimal.Parse(Console.ReadLine());
                    Console.Write("Amount: ");
                    int p_amount = int.Parse(Console.ReadLine());
                    vm.AddProduct(p_code, p_name, p_cost, p_amount);
                    break;
                case "l":
                    vm.SwitchMode2User();
                    break;
                default:
                    Console.WriteLine("Wronge code.");
                    break;
            }
        }
    }
}