# BookFlight ✈️

BookFlight is a console application for managing flights, users, and reservations. It allows administrators to add, delete, and modify flights, as well as manage users. Users can register, log in, book and cancel flights, view their reservations, and change their email addresses.

## Features
- **Admin**
    - Add, delete, and modify flights
    - Delete users
- **User Mode:**
    - Register and log in
    - Book and cancel flights
    - View reservations
    - Change email address

## Technologies Used
- **Frontend:** Console (C#)
- **Backend:** SQL, C#
- **Database:** Microsoft SQL Server Management Studio
- **Authentication:** SQL Server Authentication

## Installation

### Prerequisites
- Visual Studio
- MSSQL
- Microsoft.Data.SqlClient package
- System.Configuration.ConfigurationManager package
  

### Steps for Visual Studio 
1. **Clone the Repository:**
    ```bash
    git clone https://github.com/Niko2357/BookFlight.git
    cd BookFlight
    ```
    
2. #### **Open the solution in Visual Studio**
   Open the BookFlight.sln file in Visual Studio.
   
3. #### **Check the configuration**
   Check the App.config file and make sure it contains the correct values for connecting to the database.
   **App Configuration my database**
    ```xml
    <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
	<appSettings>
		<add key="DataSource" value="193.85.203.188"/>
		<add key="Database" value="FlyAir"/> <!-- database name-->
		<add key="UserID" value="student"/> <!-- your name-->
		<add key="Password" value="plane"/> <!-- your password-->
	</appSettings>
</configuration>
    ```
   **School database**
   ```xml
   <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
	<appSettings>
		<add key="DataSource" value="193.85.203.188"/>
		<add key="Database" value="surname"/> <!-- database name-->
		<add key="UserID" value="username"/> <!-- your name-->
		<add key="Password" value="password"/> <!-- your password-->
	</appSettings>
</configuration>
    ```
5. #### **Build the project**

6. #### **Run the application**
   Press F5 or click the "Start" button at the top of Visual Studio.


### Steps 
1. **Clone the Repository:**
    ```bash
    git clone https://github.com/Niko2357/BookFlight.git
    cd BookFlight
    ```

2. **Install Dependencies:**
    ```bash
    npm install mssql
    ```

3. **Environment Variables:**
    Create a `.env` file in the root directory or just check values in configuration file and add the following variables:
    ```env
    PORT=1433
    DB_USER=student
    DB_PASSWORD=plane
    DB_SERVER=DESKTOP-OL8TBN9\MYSERVER
    DB_DATABASE=FlyAir
    ```

4. **Run the Application:**
    ```bash
    npm start
    ```

    
## Database
FlyAir uses an SQL Server database to store information about airplanes, flights, users, and reservations. The connection details are stored in the App.config file. No manipulation or editing in database is needed. All necessary data already are contained. In this current version, project is using private database provided by author. 
The database schema includes the following tables:

- `Plane`: Stores information about planes, such as name, capacity, and producer.
- `Flight`: Stores information about flights, such as flight number, plane ID, departure and arrival times, and departure and arrival locations.
- `UserAccount`: Stores information about users, such as username, password, and email address.
- `Passenger`: Stores information about passengers, such as first name, last name, date of birth, and email address.
- `Seat`: Stores information about seats on a plane, such as seat number and availability.
- `Reservation`: Stores information about flight reservations, such as flight ID, passenger ID, seat ID, user ID, price, and payment status.

You can look at SQL file that creates these table - FlyAirSql.sql .

**Connection**

Connection is secured by Singleton design pattern. Connection specifics are written and stored in App.conf, configuration file. 
Login is required. UserId in this file is 'student' and password 'plane'. In case you want to access admin part in UI console part use Admin mode. Password for this part is 'admin'.
Admin part includes adding Planes, Flights and removing Users, Flights and Changing Flight departure time. 

**Data**
Only 3 tables include data. Tables flight.csv, planes.csv, seat.csv are located in bin/debug/net8.0 . 

## Usage

**Upon Entering**
1. Running the application, you will be presented with a menu to choose between **Admin** and **Guest** mode.
2. If you choose **Admin** mode, you will be prompted for a password. The default password is **admin**.
3. If you choose **Guest** mode, you will be taken to the user menu.
4. Follow the console instructions to navigate through the menus and perform actions.

**Functions**
1. **Search Flights:**
    - Enter departure and arrival destinations.
    - Select travel dates.
    - Click on the "Search" button.

2. **Compare Prices:**
    - View the list of available flights.
    - Compare prices and select a flight.

3. **Book Flight:**
    - Enter passenger details.
    - Make payment through the secure payment gateway.
    - Receive booking confirmation via email.


## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact
For any questions or comments, please reach out to me at:
- **Email:** niki.polach@gmail.com
- **LinkedIn:** https://linkedin.com/in/nikola-poláchová-905a342a2
- **Moodle Private Contact** https://moodle.spsejecna.cz

### https://github.com/Niko2357/BookFlight
