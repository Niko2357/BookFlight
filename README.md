# BookFlight ✈️

BookFlight is a web application designed to simplify the process of booking flights. Users can search for flights, compare prices, and make reservations with ease.

## Features
- **Flight Search:** Search for flights between any two destinations.
- **Price Comparison:** Compare prices from multiple airlines.
- **Booking:** Securely book flights and receive confirmation.
- **User Profiles:** Create and manage user profiles with booking history.
- **Notifications:** Receive email notifications for booking confirmations and updates.

## Technologies Used
- **Frontend:** React, Redux, Bootstrap
- **Backend:** Node.js, Express
- **Database:** MongoDB
- **Authentication:** JWT, OAuth
- **DevOps:** Docker, Kubernetes, GitHub Actions
- **Cloud:** AWS (EC2, S3, RDS)

## Installation

### Prerequisites
- Node.js (v14.x or later)
- MSSQL

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
No manipulation or editing in database is needed. All necessary data already are contained. In this current version, project is using private database provided by author. 

**Connection: **
Connection is secured by Singleton design pattern. Connection specifics are written and stored in App.conf, configuration file. 
Login is required. UserId in this file is 'student' and password 'plane'. In case you want to access admin part in UI console part use Admin mode. Password for this part is 'admin'.
Admin part includes adding Planes, Flights and removing Users, Flights and Changing Flight departure time. 

## Usage
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
