using KitchenRoutingSystemPOS.Data;
using KitchenRoutingSystemPOS.Models;

namespace KitchenRoutingSystemPOS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the kitchen areas
            Dictionary<string, List<string>> kitchenAreas = new Dictionary<string, List<string>>();
            kitchenAreas.Add("fries", new List<string>());
            kitchenAreas.Add("grill", new List<string>());
            kitchenAreas.Add("salad", new List<string>());
            kitchenAreas.Add("drink", new List<string>());
            kitchenAreas.Add("dessert", new List<string>());

            while (true)
            {
                // Displaying the main menu
                Console.WriteLine("Welcome to the Kitchen Routing System!");
                Console.WriteLine("1. Place an Order");
                Console.WriteLine("2. Track an Order");
                Console.WriteLine("3. Exit");
                Console.Write("Please select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Place an Order
                        Console.Write("Please enter your name: ");
                        string name = Console.ReadLine();
                        Console.Write("Please enter your order (fries, grill, salad, drink, or dessert): ");
                        string order = Console.ReadLine().ToLower();

                        if (!kitchenAreas.ContainsKey(order))
                        {
                            Console.WriteLine("Invalid order!");
                            break;
                        }

                        // Save the order to the database
                        using (var context = new KitchenContext())
                        {
                            var newOrder = new Order { CustomerName = name, Area = order, Status = "Pending" };
                            context.Orders.Add(newOrder);
                            context.SaveChanges();
                        }

                        kitchenAreas[order].Add(name);
                        Console.WriteLine("Order placed successfully!");
                        Console.WriteLine();
                        break;

                    case "2":
                        // Track an Order
                        Console.Write("Please enter your name: ");
                        name = Console.ReadLine();

                        // Retrieve the order from the database
                        using (var context = new KitchenContext())
                        {
                            var orderQuery = context.Orders.FirstOrDefault(o => o.CustomerName == name);
                            if (orderQuery == null)
                            {
                                Console.WriteLine("Invalid name!");
                                break;
                            }

                            order = orderQuery.Area;
                            string status = orderQuery.Status;

                            Console.WriteLine($"Your order is being prepared in the {order} area of the kitchen.");
                            Console.WriteLine($"Current queue: {string.Join(", ", kitchenAreas[order])}");
                            Console.WriteLine($"Status: {status}");
                            Console.WriteLine();
                        }
                        break;

                    case "3":
                        // Exit
                        Console.WriteLine("Thank you for Ordering, Come Again!!");
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}