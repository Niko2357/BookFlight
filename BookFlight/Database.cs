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

        /// <summary>
        /// Adds a plane to the database.
        /// </summary>
        /// <param name="plane"></param>
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

        /// <summary>
        /// Adds a flight to the database.
        /// </summary>
        /// <param name="flight"></param>
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
        }

        /// <summary>
        /// Removes a plane from the database.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveFlight(int id)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM Flight WHERE id = @FlightId;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@flightId", id);

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

        /// <summary>
        /// Lists all flights in the database.
        /// </summary>
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
            conn.Close();
        }

        /// <summary>
        /// Lists all planes in the database.
        /// </summary>
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
            conn.Close();
        }

        /// <summary>
        /// Lists all reservations in the database.
        /// </summary>
        /// <param name="userId"></param>
        public void AllReservations(int userId)
        {
            if(userId == 0)
            {
                Console.WriteLine("You have to log in.");
            } 

            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id, flight.flightNumber, flight.departure, flight.arrival, flight.departurePlace, flight.arrivalPlace, seat.seatNumber, price, isPaid " +
                "FROM Reservation INNER JOIN Flight flight on reservation.flightId = flight.id  INNER JOIN Seat seat on reservation.seatId = seat.id WHERE userId = @userID;";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@userID", userId);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Flight number   |  Departure /  Arrival  |   From  ->   To  |   Seat  | Price |  Transaction");
                        Console.WriteLine($"{reader["id"]}  | {reader["flight.flightNumber"]} | {reader["flight.departure"]} / {reader["flight.arrival"]}  | {reader["flight.departurePlace"]} -> {reader["flight.arrivalPlace"]}  | {reader["seat.SeatNumber"]} | {reader["price"]}  | {reader["isPaid"]}");
                    }
                }
            }
            conn.Close();
        }

        /// <summary>
        /// Changes the departure and arrival time of a Flight.
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        public void AlterFlight(int flightId, DateTime departure, DateTime arrival)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "UPDATE Flight SET departure = @departiure arrival = @arrival WHERE id = @flightId;";
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@departure", departure);
                cmd.Parameters.AddWithValue("@arrival", arrival);
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

        /// <summary>
        /// Adds a reservation to the database.
        /// </summary>
        /// <param name="reservation"></param>
        public void AddReservation(Reservation reservation)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "INSERT INTO Reservation (flightId, passengerId, userId, seatId, price, isPaid) VALUES (@flightId, @passengerId, @userId, @seatId, @price, @isPaid);";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@flightId", reservation.flightId);
                cmd.Parameters.AddWithValue("@passengerId", reservation.passengerId);
                cmd.Parameters.AddWithValue("@userId", reservation.userId);
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

        /// <summary>
        /// Changes the availability of a seat.
        /// </summary>
        /// <param name="id"></param>
        public void AlterSeat(int id)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "UPDATE Seat SET isAvailable = @false WHERE id = @flightId;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@false", false);
                cmd.Parameters.AddWithValue("@flightId", id);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Seat Successfuly updated.");
                }
                else
                {
                    Console.WriteLine("This Seat doesn't exist.");
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Removes a reservation from the database.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveReservation(int id)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM Reservation WHERE id = @ReservationId;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@ReservationId", id);

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

        /// <summary>
        /// Adds a passenger to the database.
        /// </summary>
        /// <param name="passenger"></param>
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

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Removes a user from the database.
        /// </summary>
        /// <param name="username"></param>
        public void RemoveUser(string username)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "DELETE FROM UserAccount WHERE username = @Username;";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);

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

        /// <summary>
        /// Changes the email of a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newMail"></param>
        public void AlterUser(string username, string newMail)
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "UPDATE UserAccount SET email = @email WHERE username = @username";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@email", newMail);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// <summary>
        /// Lists all users in the database.
        /// </summary>
        public void AllUsers()
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT id, username FROM UserAccount;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Username");
                        Console.WriteLine($"{reader["id"]}  | {reader["username"]}");

                    }
                }
            }
            conn.Close();
        }

        /// <summary>
        /// Lists all Seats in the database.
        /// </summary>
        public void AllSeats()
        {
            SqlConnection conn = DBSingleton.GetInstance();
            string com = "SELECT seatNumber, isAvailable FROM Seat;";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Seat Number |  Availability");
                        Console.WriteLine($"{reader["seatNumber"]}  | {reader["isAvailable"]}");

                    }
                }
            }
            conn.Close();
        }

        /// <summary>
        /// Imports data into Planes table from a file.
        /// </summary>
        /// <param name="filepath"></param>
        public void ImportPlanes(string filepath)
        {
            try
            {
                using(StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var head = reader.ReadLine();
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var com = "INSERT INTO Plane (id, name, capacity, producer) VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + "'," + values[3] +");";
                        SqlConnection conn = DBSingleton.GetInstance();
                        using (SqlCommand cmd = new SqlCommand(com, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Imports data into Flights table from a file.
        /// </summary>
        /// <param name="filepath"></param>
        public void ImportFlight(string filepath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var head = reader.ReadLine();
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var com = "INSERT INTO Flight (id,flightNumber, planeId, departure, arrival, departurePlace, arrivalPlace) VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + "'," + values[3] + "'," + values[4] + "'," + values[5] + "'," + values[6] + ");";
                        SqlConnection conn = DBSingleton.GetInstance();
                        using (SqlCommand cmd = new SqlCommand(com, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Imports data into Seats table from a file.
        /// </summary>
        /// <param name="filepath"></param>
        public void ImportSeat(string filepath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var head = reader.ReadLine();
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var com = "INSERT INTO Seat (flightId, seatNumber, isAvailable) VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + ");";
                        SqlConnection conn = DBSingleton.GetInstance();
                        using (SqlCommand cmd = new SqlCommand(com, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
