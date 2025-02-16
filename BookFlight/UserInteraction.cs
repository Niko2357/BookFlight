using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class UserInteraction
    {
        public UserInteraction() { }

        Database db = new Database();
        public void AdminPart()
        {
            Console.WriteLine("1) Add a Flight");
            Console.WriteLine("2) Delete a Flight");
            Console.WriteLine("3) Delete an User");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("What's the flight number?");
                string flnum = Console.ReadLine();
                Console.WriteLine("What plane will fly this?");


                Flight flight = new Flight();
                db.AddFlight();
            }
            else if (choice == "2")
            {
                Console.WriteLine();
                db.RemoveFlight();
            }
            else if (choice == "3")
            {
                Console.WriteLine();
                db.RemoveUser();
            }
            else
            {
                Console.WriteLine("That's not an option.");

            }
        }

        public void UserPart()
        {
            Console.WriteLine("1) Register");
            Console.WriteLine("2) Log in");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                LogIn();
            }
            else if (choice == "2")
            {
                Register();
            }
            else
            {
                Console.WriteLine("That's not an option.");
                UserPart();
            }
        }

        public void LogIn()
        {

        }

        public void Register()
        {

        }
    }
}
