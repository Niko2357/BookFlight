using Microsoft.Data.SqlClient;
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
        private int thisUser = 0;
        /// <summary>
        /// Menu with options for administrator of flights available. 
        /// When choice is made, admin is asked needed information for method to perform wanted task. 
        /// All methods communicate with database and edit it. 
        /// </summary>
        public void AdminPart(SqlConnection conn)
        {
            Console.WriteLine("1) Add a Flight");
            Console.WriteLine("2) Delete a Flight");
            Console.WriteLine("3) Delete an User");
            Console.WriteLine("4) Change flight departure");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("What's the flight number?");
                string flnum = Console.ReadLine();
                db.AllPlanes(conn);
                Console.WriteLine("What plane will fly this? (id number)");
                int plane = int.Parse(Console.ReadLine());
                Console.WriteLine("What's departure date and time? (dd.mm.yyyy hh:mm:ss)");
                DateTime dep = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What's arrival date and time? (dd.mm.yyyy hh:mm:ss)");
                DateTime arr = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Where is it flying from?");
                string depPlace = Console.ReadLine();
                Console.WriteLine("What's the destination?");
                string arrPlace = Console.ReadLine();

                Flight flight = new Flight(flnum, plane, dep, arr, depPlace, arrPlace);
                db.AddFlight(flight, conn);
            }
            else if (choice == "2")
            {
                db.AllFlights(conn);
                Console.WriteLine("What flight do you want to remove? (id number)");
                int id = int.Parse(Console.ReadLine());
                db.RemoveFlight(id, conn);
            }
            else if (choice == "3")
            {
                db.AllUsers(conn);
                Console.WriteLine("Which user would you like to remove?");
                string username = Console.ReadLine();

                db.RemoveUser(username, conn);
            }
            else if(choice == "4")
            {
                db.AllFlights(conn);
                Console.WriteLine("Which flight time would you like to update? (id)");
                int flightId = int.Parse(Console.ReadLine());
                Console.WriteLine("What time will the plane depart? (dd.mm.yyyy hh:mm:ss)");
                DateTime depart = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What time will the plane arrive? (dd.mm.yyyy hh:mm:ss)");
                DateTime arrive = DateTime.Parse(Console.ReadLine());
                db.AlterFlight(flightId, depart, arrive, conn);
            }
            else
            {
                Console.WriteLine("That's not an option.");
                AdminPart(conn);
            }
        }

        /// <summary>
        /// Menu with options for user. User has to log in or register to proceed.
        /// </summary>
        public void UserPart(SqlConnection conn)
        {
            Console.WriteLine("1) Register");
            Console.WriteLine("2) Log in");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Register(conn);
            }
            else if (choice == "2")
            {
                LogIn(conn);
            }
            else
            {
                Console.WriteLine("That's not an option.");
                UserPart(conn);
            }
        }

        /// <summary>
        /// Menu with options for user. User can book a flight, cancel a flight, see his reservations or change email.
        /// </summary>
        public void UserMainPart(SqlConnection conn)
        {
            Console.WriteLine("1) Book a Flight");
            Console.WriteLine("2) Cancel a Flight");
            Console.WriteLine("3) My Reservations");
            Console.WriteLine("4) Change email");
            Console.WriteLine("5) See Flights");
            Console.WriteLine("6) See Planes");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Has this person already flown with us?");
                string input = Console.ReadLine();

                Console.WriteLine("What email should be the boarding pass send to? *every passenger should have their own email");
                string email = Console.ReadLine();
                string firstname = "";
                string lastname = "";
                DateOnly born = new DateOnly();

                if (CheckPassenger(email, conn))
                {
                    Console.WriteLine("You will need passenger number to proceed with booking!");
                }
                else
                {
                    Console.WriteLine("What's first name of the passenger?");
                    firstname = Console.ReadLine();
                    Console.WriteLine("What's last name of the passenger?");
                    lastname = Console.ReadLine();
                    Console.WriteLine("When was the passenger born? (dd.mm.yyyy)");
                    born = new DateOnly();
                }
                Passenger pas = new Passenger(firstname, lastname, born, email);
                db.AddPassenger(pas, conn);

                db.AllFlights(conn);
                Console.WriteLine("What Flight would you like to book a reservation for? (id)");
                int flightId = int.Parse(Console.ReadLine());
                db.AllSeats(conn);
                Console.WriteLine("What seat would you like to book? (id)");
                int seat = int.Parse(Console.ReadLine());
                db.AlterSeat(seat, conn);
                Reservation res = new Reservation(flightId, pas.id, seat, thisUser, 2453.5, false);

                db.AddReservation(res, conn);
            }
            else if (choice == "2")
            {
                db.AllReservations(thisUser, conn);
                Console.WriteLine("Which reservation would you like to cancel? (id)");
                int id = int.Parse(Console.ReadLine());
                db.RemoveReservation(id, conn);
            }
            else if (choice == "3")
            {
                db.AllReservations(thisUser, conn);
            }
            else if (choice == "4")
            {
                Console.WriteLine("Enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("What's your new email?");
                string email = Console.ReadLine();
                db.AlterUser(username, email, conn);
            }
            else if (choice == "5")
            {
                db.AllFlights(conn);
            }
            else if (choice == "6")
            {
                db.AllPlanes(conn);
            }
            else
            {
                Console.WriteLine("That's not an option.");
                UserPart(conn);
            }
        }

        /// <summary>
        /// Checks if passenger's email is already in database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckPassenger(string email, SqlConnection conn)
        {
            string com = "SELECT id FROM Passenger WHERE email = @email;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                object existingPasId = cmd.ExecuteScalar();
                if (existingPasId != null)
                {
                    Console.WriteLine("Passengers number is " + existingPasId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Logs in user. If input is correct, user is logged in and can proceed to UserMainPart.
        /// </summary>
        public void LogIn(SqlConnection conn)
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            string com = "SELECT id FROM UserAccount WHERE username = @username AND password = @password;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                //this part from chat
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int idUser = Convert.ToInt32(result);
                    Console.WriteLine("You're now loged in.");
                    thisUser = idUser;
                    UserMainPart(conn);
                }
                else
                {
                    Console.WriteLine("Wrong input.");
                    UserPart(conn);
                }
            }
        }

        /// <summary>
        /// Registers user. User has to input username, password and email.
        /// </summary>
        public void Register(SqlConnection conn)
        {
            Console.WriteLine("Hello, here you can register.");
            Console.WriteLine("Your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Your password:");
            string password = Console.ReadLine();
            Console.WriteLine("Your email:");
            string email = Console.ReadLine();

            string com = "INSERT INTO UserAccount (username, password, email) VALUES (@username, @password, @email);";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("You were successfuly registered. Please Log in");
                    UserPart(conn);
                }
                else
                {
                    Console.WriteLine("Something went wrong :(");
                }
            }
        }
    }
}
