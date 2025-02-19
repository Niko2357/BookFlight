using Microsoft.Data.SqlClient;

namespace BookFlight
{
    public class Program
    {
        static void Main(string[] args)
        {

            UserInteraction interact = new UserInteraction();
            Database dbs = new Database();


            /*
            SqlConnection conn = DBSingleton.GetInstance();
            try
            {
                Console.WriteLine("Connected successfuly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No connection.");
            }
            */

            try
            {
                SqlConnection conn = DBSingleton.GetInstance();
                ///importing of data from csv files
                //dbs.ImportPlanes("planes.csv", conn);
                //dbs.ImportFlight("flight.csv", conn);
                dbs.ImportSeat("seat.csv", conn);


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
                        interact.AdminPart(conn);
                    }
                    else
                    {
                        Console.WriteLine("You aren't an admin.");
                        interact.UserPart(conn);
                    }
                }
                else if (choice == "2")
                {
                    interact.UserPart(conn);
                }
                else
                {
                    Console.WriteLine("There's no such an option.");
                }
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
