using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Database
    {
        public Database() { }

        public void AddPlane(Plane plane)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO Plane (name, capacity, producer) VALUES (@name, @capacity, @producer);";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@name", plane.name);
                cmd.Parameters.AddWithValue("@capacity", plane.capacity);
                cmd.Parameters.AddWithValue("@producer", plane.producer);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Plane Successfuly added.");
                }
                else
                {
                    Console.WriteLine("This Plane couldn't be added.");
                }
                conn.Close();
            }
        }

        public void AddFlight(Flight flight)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO Flight (flightNumber, planeId, departure, arrival, departurePlace, arrivalPlace) VALUES (@flightNumber, @planeId, @departure, @arrival, @departurePlace, @arrivalPlace);";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@flightNumber", flight.flightNumber);
                cmd.Parameters.AddWithValue("@planeId", flight.planeId);
                cmd.Parameters.AddWithValue("@departure", flight.departure);
                cmd.Parameters.AddWithValue("@arrival", flight.arrival);
                cmd.Parameters.AddWithValue("@departurePlace", flight.departurePlace);
                cmd.Parameters.AddWithValue("@arrivalPlace", flight.arrivalPlace);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Flight Successfuly added.");
                }
                else
                {
                    Console.WriteLine("This Flight couldn't be added.");
                }
                conn.Close();
            }
            conn.Close();
        }

        public void RemoveFlight(Flight flight)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM Flight WHERE id = @FlightId;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@flightId", flight.id);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Flight Successfuly deleted.");
                }
                else
                {
                    Console.WriteLine("This flight doesn't exist.");
                }
                conn.Close();
            }
        }

        public void AllFlights()
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id, flightNumber, departure, arrival, departurePlace, arrivalPlace from Flight;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Flight Number  | Departure /  Arrival  |   From  ->   To");
                        Console.WriteLine($"{reader["id"]}  | {reader["flightNumber"]} | {reader["departure"]} / {reader["arrival"]} | {reader["deparure"]} ->  {reader["arrival"]}");

                    }
                }
            }
        }

        public void AllPlanes()
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id, name, capacity, producer from Plane;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Plane  | Capacity  |  Manufacturer");
                        Console.WriteLine($"{reader["id"]}  | {reader["name"]} | {reader["capacity"]}  | {reader["producer"]}");

                    }
                }
            }
        }

        public void AllReservations(int userId)
        {
            if(userId == 0)
            {
                Console.WriteLine("You have to log in.");
            } 

            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id, flight.flightNumber, flight.departure, flight.arrival, flight.departurePlace, flight.arrivalPlace, seat.seatNumber, price, isPaid FROM Reservation INNER JOIN Flight flight on reservation.flightId = flight.id  INNER JOIN Seat seat on reservation.seatId = seat.id WHERE userId = @userID;";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@userID", userId);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Flight number   |  Departure /  Arrival  |   From  ->   To  |   Seat  | Price |  Transaction   |");
                        Console.WriteLine($"{reader["id"]}  | {reader["flight.flightNumber"]} | {reader["flight.departure"]} / {reader["flight.arrival"]}  | {reader["flight.departurePlace"]} -> {reader["flight.arrivalPlace"]}  | {reader["seat.SeatNumber"]} | {reader["price"]}  | {reader["isPaid"]}");
                    }
                }
            }
        }

        public void AlterFlight(int flightId, DateTime dateTime)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "UPDATE Flight SET departure_time = @dateTime WHERE flight_id = @flightId;";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@dateTime", dateTime);
                cmd.Parameters.AddWithValue("@flightId", flightId);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Flight Successfuly updated.");
                }
                else
                {
                    Console.WriteLine("This flight doesn't exist.");
                }
                conn.Close();
            }
        }

        public void AddReservation(Reservation reservation)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO Reservation (flightId, passengerId, seatId, price, isPaid) VALUES (@flightId, @passengerId, @seatId, @price, @isPaid);";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@flightId", reservation.flightId);
                cmd.Parameters.AddWithValue("@passengerId", reservation.passengerId);
                cmd.Parameters.AddWithValue("@seatId", reservation.seatId);
                cmd.Parameters.AddWithValue("@price", reservation.price);
                cmd.Parameters.AddWithValue("@isPaid", reservation.isPaid);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Reservation Successfuly added.");
                }
                else
                {
                    Console.WriteLine("This Reservation couldn't be made.");
                }
                conn.Close();
            }
        }

        public void RemoveReservation(Reservation reservation)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM Reservation WHERE id = @ReservationId;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@ReservationId", reservation.id);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Reservation Successfuly deleted.");
                }
                else
                {
                    Console.WriteLine("This Reservation doesn't exist.");
                }
                conn.Close();
            }
        }

        public void AddPassenger(Passenger passenger)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO Passenger (firstname, surname, birthDate, email) VALUES (@firstname, @surname, @birthDate, @email);";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@firstname", passenger.firstname);
                cmd.Parameters.AddWithValue("@surname", passenger.surname);
                cmd.Parameters.AddWithValue("@birthDate", passenger.birthDate);
                cmd.Parameters.AddWithValue("@email", passenger.email);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Passenger Successfuly added.");
                }
                else
                {
                    Console.WriteLine("This Passenger doesn't exist.");
                }
                conn.Close();
            }
        }

        public void AddUser(UserAccount user)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO UserAccount (username, password) VALUES (@username, @password);";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("User Successfuly added.");
                }
                else
                {
                    Console.WriteLine("This User doesn't exist.");
                }
                conn.Close();
            }
        }

        public void RemoveUser(UserAccount user)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM UserAccount WHERE username = @Username;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.username);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("User Successfuly removed.");
                }
                else
                {
                    Console.WriteLine("This User doesn't exist.");
                }

                conn.Close();
            }
        }

        public void AlterPassenger(Passenger passenger, string newMail)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "UPDATE Passenger SET email = @email WHERE id = @passengerId";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@email", newMail);
                cmd.Parameters.AddWithValue("@passengerId", passenger.id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
