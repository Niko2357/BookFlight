using Microsoft.Data.SqlClient;

namespace BookFlight
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*try
            {
                using (SqlConnection connection = DBSingleton.GetInstance())
                {
                    Console.WriteLine("Připojeno k databázi!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/

            UserInteraction interact = new UserInteraction();

            try
            {
                Console.WriteLine("1) Admin");
                Console.WriteLine("2) Guest");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();
                    if (password == "admin")
                    {
                        Console.WriteLine("Log in successful");
                        interact.AdminPart();
                    }
                    else
                    {
                        Console.WriteLine("You aren't an admin.");
                        interact.UserPart();
                    }
                }
                else if (choice == "2")
                {
                    interact.UserPart();
                }
                else
                {
                    Console.WriteLine("There's no such an option.");
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
