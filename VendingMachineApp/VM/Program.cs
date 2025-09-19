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
                Console.WriteLine("2 - Insert coins");
                Console.WriteLine("3 - Buy product [wait for answer]");
                Console.WriteLine("4 - Take money back");
                Console.WriteLine("5 - Enter admin mode");
                Console.WriteLine("q - Quit");

            }
            else
            {
                Console.WriteLine("\n101 - add quantity of product");
                Console.WriteLine("102 - add new product [not ready, check for unique code, chack for input type]");
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
                    vm.ReturnMoney2User();
                    break;
                case "5":
                    Console.Write("Enter admin code: ");
                    string key = Console.ReadLine();
                    vm.SetMode(key);
                    break;
                case "q":
                    return;
                case "101":
                    vm.AddAmountProduct();
                    break;
                case "102":
                    vm.AddProduct();
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