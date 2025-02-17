﻿using Microsoft.Data.SqlClient;
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
        public void AdminPart()
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
                Console.WriteLine("What plane will fly this? (id number)");
                db.AllPlanes();
                int plane = int.Parse(Console.ReadLine());
                Console.WriteLine("What's departure date and time? (dd.mm.yyyy hh.mm)");
                DateTime dep = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What's arrival date and time? (dd.mm.yyyy hh.mm)");
                DateTime arr = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Where is it flying from?");
                string depPlace = Console.ReadLine();
                Console.WriteLine("What's the destination?");
                string arrPlace = Console.ReadLine();

                Flight flight = new Flight(flnum, plane, dep, arr, depPlace, arrPlace);
                db.AddFlight(flight);
            }
            else if (choice == "2")
            {
                db.AllFlights();
                Console.WriteLine("What flight do you want to remove? (id number)");
                int id = int.Parse(Console.ReadLine());
                db.RemoveFlight(id);
            }
            else if (choice == "3")
            {
                db.AllUsers();
                Console.WriteLine("Which user would you like to remove?");
                string username = Console.ReadLine();

                db.RemoveUser(username);
            }
            else if(choice == "4")
            {
                db.AllFlights();
                Console.WriteLine("Which flight time would you like to update? (id)");
                int flightId = int.Parse(Console.ReadLine());
                Console.WriteLine("What time will the plane depart? (dd.mm.yyyy hh.mm)");
                DateTime depart = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What time will the plane arrive? (dd.mm.yyyy hh.mm)");
                DateTime arrive = DateTime.Parse(Console.ReadLine());
                db.AlterFlight(flightId, depart, arrive);
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
                Register();
            }
            else if (choice == "2")
            {
                LogIn();
            }
            else
            {
                Console.WriteLine("That's not an option.");
                UserPart();
            }
        }

        public void UserMainPart()
        {
            Console.WriteLine("1) Book a Flight");
            Console.WriteLine("2) Cancel a Flight");
            Console.WriteLine("3) My Reservations");
            Console.WriteLine("4) Change email");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Has this person already flown with us?");
                string input = Console.ReadLine();

                Console.WriteLine("What email should be the boarding pass send to?");
                string email = Console.ReadLine();

                if (CheckPassenger(email))
                {
                    Console.WriteLine("You will need passenger number to proceed with booking!");
                }
                else
                {
                    Console.WriteLine("What's first name of the passenger?");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("What's last name of the passenger?");
                    string lastname = Console.ReadLine();
                    Console.WriteLine("When was the passenger born? (dd.mm.yyyy)");
                    DateOnly born = new DateOnly();

                    Passenger pas = new Passenger(firstname, lastname, born, email);
                    db.AddPassenger(pas);
                }
                
                Console.WriteLine();
                db.AddReservation();
            }
            else if (choice == "2")
            {
                db.RemoveReservation();
            }
            else if(choice == "3")
            {
                db.AllReservations(thisUser);
            }
            else if(choice == "4")
            {
                db.AlterUser();
            }
            else
            {
                Console.WriteLine("That's not an option.");
                UserPart();
            }
        }

        public bool CheckPassenger(string email)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id FROM Passenger WHERE email = @email;";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
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
            conn.Close();
        }

        public void LogIn()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id FROM UserAccount WHERE username = @username AND password = @password;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                object result = cmd.ExecuteScalar();
                if(result != null)
                {
                    int idUser = Convert.ToInt32(result);
                    Console.WriteLine("You're now loged in.");
                    thisUser = idUser;
                    UserMainPart();
                }
                else
                {
                    Console.WriteLine("Wrong input.");
                    UserPart();
                }
            }
        }

        public void Register()
        {
            Console.WriteLine("Hello, here you can register.");
            Console.WriteLine("Your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Your password:");
            string password = Console.ReadLine();
            Console.WriteLine("Your email:");
            string email = Console.ReadLine();

            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO UserAccount (username, password, email) VALUES (@username, @password, @email);";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    Console.WriteLine("You were successfuly registered. Please Log in");
                    UserPart();
                }
                else
                {
                    Console.WriteLine("Something went wrong :(");
                }
            }
        }
    }
}
