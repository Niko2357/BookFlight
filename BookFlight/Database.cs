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
        /// <param name="conn"></param>
        public void AddPlane(Plane plane, SqlConnection conn)
        {
            string com = "INSERT INTO Plane (name, capacity, producer) VALUES (@name, @capacity, @producer);";

            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@name", plane.name);
                cmd.Parameters.AddWithValue("@capacity", plane.capacity);
                cmd.Parameters.AddWithValue("@producer", plane.producer);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    Console.WriteLine("Plane successfully added.");
                }
                else
                {
                    Console.WriteLine("This plane couldn't be added.");
                }
            }
        }


        /// <summary>
        /// Adds a flight to the database.
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="conn"></param>
        public void AddFlight(Flight flight, SqlConnection conn)
        {
            string com = "INSERT INTO Flight (flightNumber, planeId, departure, arrival, departurePlace, arrivalPlace) " +
                            "VALUES (@flightNumber, @planeId, @departure, @arrival, @departurePlace, @arrivalPlace);";

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
                    Console.WriteLine("Flight successfully added.");
                }
                else
                {
                    Console.WriteLine("This flight couldn't be added.");
                }
            }
        }

        /// <summary>
        /// Adds a reservation to the database.
        /// </summary>
        /// <param name="reservation"></param>
        public void AddReservation(Reservation reservation, SqlConnection conn)
        {
            string com = "INSERT INTO Reservation (flightId, passengerId, userId, seatId, price, isPaid) VALUES (@flightId, @passengerId, @userId, @seatId, @price, @isPaid);";
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
            }
        }

        /// <summary>
        /// Adds a passenger to the database.
        /// </summary>
        /// <param name="passenger"></param>
        public void AddPassenger(Passenger passenger, SqlConnection conn)
        {
            string com = "INSERT INTO Passenger (firstname, surname, birthDate, email) VALUES (@firstname, @surname, @birthDate, @email);";
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
            }
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="conn"></param>
        public void AddUser(UserAccount user, SqlConnection conn)
        {
            string com = "INSERT INTO UserAccount (username, password) VALUES (@username, @password);";
            using (SqlCommand cmd = new SqlCommand(com, conn))
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
            }
        }


        /// <summary>
        /// Removes a plane from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conn"></param>
        public void RemoveFlight(int id, SqlConnection conn)
        {
            string com = "DELETE FROM Flight WHERE id = @FlightId;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@FlightId", id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Flight successfully deleted." : "This flight doesn't exist.");
            }
        }

        /// <summary>
        /// Removes a user from the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="conn"></param>
        public void RemoveUser(string username, SqlConnection conn)
        {
            string com = "DELETE FROM UserAccount WHERE username = @Username;";
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
            }
        }

        /// <summary>
        /// Removes a reservation from the database.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveReservation(int id, SqlConnection conn)
        {
            string com = "DELETE FROM Reservation WHERE id = @ReservationId;";
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
            }
        }



        /// <summary>
        /// Retrieves and displays all flights from the database.
        /// </summary>
        /// <param name="conn"></param>
        public void AllFlights(SqlConnection conn)
        {
            string com = "SELECT id, flightNumber, departure, arrival, departurePlace, arrivalPlace FROM Flight;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID  |  Flight Number  | Departure /  Arrival  |   From  ->   To");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id"]}  | {reader["flightNumber"]} | {reader["departure"]} / {reader["arrival"]} | {reader["departurePlace"]} -> {reader["arrivalPlace"]}");
                    }
                }
            }
        }

        /// <summary>
        /// Updates the departure and arrival times for a flight.
        /// </summary>
        /// <param name="conn"></param>
        public void AlterFlight(int flightId, DateTime departure, DateTime arrival, SqlConnection conn)
        {
            string com = "UPDATE Flight SET departure = @departure, arrival = @arrival WHERE id = @flightId;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@departure", departure);
                cmd.Parameters.AddWithValue("@arrival", arrival);
                cmd.Parameters.AddWithValue("@flightId", flightId);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Flight successfully updated." : "This flight doesn't exist.");
            }
        }

        /// <summary>
        /// Updates seat availability in the database.
        /// </summary>
        /// <param name="conn"></param>
        public void AlterSeat(int id, SqlConnection conn)
        {
            string com = "UPDATE Seat SET isAvailable = @isAvailable WHERE id = @seatId;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@isAvailable", false);
                cmd.Parameters.AddWithValue("@seatId", id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Seat successfully updated." : "This seat doesn't exist.");
            }
        }

        /// <summary>
        /// Changes the email of a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newMail"></param>
        /// <param name="conn"></param>
        public void AlterUser(string username, string newMail, SqlConnection conn)
        {
            string com = "UPDATE UserAccount SET email = @email WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@email", newMail);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Lists all users in the database.
        /// </summary>
        /// <param name="conn"></param>
        public void AllUsers(SqlConnection conn)
        {
            string com = "SELECT id, username FROM UserAccount;";
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
        }

        /// <summary>
        /// Lists all Seats in the database.
        /// </summary>
        /// <param name="conn"></param>
        public void AllPlanes(SqlConnection conn)
        {
            string com = "SELECT id, name, capacity, producer FROM Plane;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID |  Name  | Capacity |  Manufacturer");
                        Console.WriteLine($"{reader["id"]}  | {reader["name"]} | {reader["capacity"]} | {reader["producer"]}");
                    }
                }
            }
        }

        /// <summary>
        /// Lists all reservations in the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="conn"></param>
        public void AllReservations(int userId, SqlConnection conn)
        {
            string com = "SELECT id, flight.flightNumber, flight.departure, flight.arrival, flight.departurePlace, flight.arrivalPlace, seat.seatNumber, price, isPaid " +
            "FROM Reservation INNER JOIN Flight flight on reservation.flightId = flight.id  INNER JOIN Seat seat on reservation.seatId = seat.id WHERE userId = @userID;";
            using (SqlCommand cmd = new SqlCommand(com, conn))
            {
                cmd.Parameters.AddWithValue("@userID", userId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID  |  Flight number   |  Departure /  Arrival  |   From  ->   To  |   Seat  | Price |  Transaction");
                        Console.WriteLine($"{reader["id"]}  | {reader["flight.flightNumber"]} | {reader["flight.departure"]} / {reader["flight.arrival"]}  | {reader["flight.departurePlace"]} -> {reader["flight.arrivalPlace"]}  | {reader["seat.SeatNumber"]} | {reader["price"]}  | {reader["isPaid"]}");
                    }
                }
            }
        }

        /// <summary>
        /// Lists all Seats in the database.
        /// </summary>
        /// <param name="conn"></param>
        public void AllSeats(SqlConnection conn)
        {
            string com = "SELECT seatNumber, isAvailable FROM Seat;";
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
        }

        /// <summary>
        /// Imports data into Planes table from a csv file.
        /// Partly chats work, the checking.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="conn"></param>
        public void ImportPlanes(string filepath, SqlConnection conn)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string head = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        string checkExistence = "SELECT COUNT(*) FROM Plane WHERE id = @id";
                        using (SqlCommand cmdCheck = new SqlCommand(checkExistence, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@id", values[0]);
                            int count = (int)cmdCheck.ExecuteScalar();
                            if (count == 0)
                            {
                                string com = "INSERT INTO Plane (id, name, capacity, producer) " +
                                             "VALUES (@id, @name, @capacity, @producer)";
                                using (SqlCommand cmd = new SqlCommand(com, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", values[0]);
                                    cmd.Parameters.AddWithValue("@name", values[1]);
                                    cmd.Parameters.AddWithValue("@capacity", values[2]);
                                    cmd.Parameters.AddWithValue("@producer", values[3]);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Imports data into Flights table from a csv file.
        /// Partly chats work, the checking.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="conn"></param>
        public void ImportFlight(string filepath, SqlConnection conn)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string head = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        string checkExistence = "SELECT COUNT(*) FROM Flight WHERE id = @id";
                        using (SqlCommand cmdCheck = new SqlCommand(checkExistence, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@id", values[0]);
                            int count = (int)cmdCheck.ExecuteScalar();
                            if (count == 0)
                            {
                                string com = "INSERT INTO Flight (id, flightNumber, planeId, departure, arrival, departurePlace, arrivalPlace) " +
                                             "VALUES (@id, @flightNumber, @planeId, @departure, @arrival, @departurePlace, @arrivalPlace);";
                                using (SqlCommand cmd = new SqlCommand(com, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", values[0]);
                                    cmd.Parameters.AddWithValue("@flightNumber", values[1]);
                                    cmd.Parameters.AddWithValue("@planeId", values[2]);
                                    cmd.Parameters.AddWithValue("@departure", values[3]);
                                    cmd.Parameters.AddWithValue("@arrival", values[4]);
                                    cmd.Parameters.AddWithValue("@departurePlace", values[5]);
                                    cmd.Parameters.AddWithValue("@arrivalPlace", values[6]);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Imports data into Seats table from a csv file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="conn"></param>
        public void ImportSeat(string filepath, SqlConnection conn)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string head = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        if (values.Length != 4)
                        {
                            Console.WriteLine($"Chyba: Řádek nemá dostatek hodnot! ({line})");
                            continue; // Přeskočí tento řádek
                        }

                        string checkExistence = "SELECT COUNT(*) FROM Seat WHERE id = @id";
                        using (SqlCommand cmdCheck = new SqlCommand(checkExistence, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@id", values[0]);
                            cmdCheck.Parameters.AddWithValue("@flightId", values[1]);
                            cmdCheck.Parameters.AddWithValue("@seatNumber", values[2]);
                            int count = (int)cmdCheck.ExecuteScalar();
                            if (count == 0)
                            {
                                string com = "INSERT INTO Seat (id, flightId, seatNumber, isAvailable) " +
                                             "VALUES (@id, @flightId, @seatNumber, @isAvailable)";
                                using (SqlCommand cmd = new SqlCommand(com, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", values[0]);
                                    cmd.Parameters.AddWithValue("@flightId", values[1]);
                                    cmd.Parameters.AddWithValue("@seatNumber", values[2]);
                                    bool avail = bool.Parse(values[3]);
                                    cmd.Parameters.AddWithValue("@isAvailable", avail);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
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
