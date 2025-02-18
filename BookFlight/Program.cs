using Microsoft.Data.SqlClient;

namespace BookFlight
{
    public class Program
    {
        static void Main(string[] args)
        {

            UserInteraction interact = new UserInteraction();
            Database dbs = new Database();

            /*using (SqlConnection conn = new SqlConnection(connectionString))
            {
            try
            {
                conn.Open();
                Console.WriteLine("Connected successfuly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No connection.");
            }
            }
            }*/


            try
            {
                ///importing of data from csv files
                /*using (SqlConnection conn = DBSingleton.GetInstance())
                {
                    dbs.ImportPlanes("planes.csv", conn);
                    dbs.ImportFlight("flight.csv", conn);
                    dbs.ImportSeat("seat.csv", conn);
                }*/

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
