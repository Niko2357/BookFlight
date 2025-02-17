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
- MongoDB
- Docker (optional for containerization)

### Steps
1. **Clone the Repository:**
    ```bash
    git clone https://github.com/Niko2357/BookFlight.git
    cd BookFlight
    ```

2. **Install Dependencies:**
    ```bash
    npm install
    ```

3. **Environment Variables:**
    Create a `.env` file in the root directory and add the following variables:
    ```env
    PORT=3000
    MONGODB_URI=your_mongodb_uri
    JWT_SECRET=your_jwt_secret
    ```

4. **Run the Application:**
    ```bash
    npm start
    ```

5. **Docker (Optional):**
    Build and run the application using Docker:
    ```bash
    docker build -t bookflight .
    docker run -p 3000:3000 bookflight
    ```

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

## Contributing
We welcome contributions from the community! Please follow these steps to contribute:

1. **Fork the Repository**
2. **Create a Branch:**
    ```bash
    git checkout -b feature/your-feature-name
    ```
3. **Make Changes and Commit:**
    ```bash
    git commit -m "Add your commit message"
    ```
4. **Push to the Branch:**
    ```bash
    git push origin feature/your-feature-name
    ```
5. **Create a Pull Request**

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact
For any questions or inquiries, please reach out to me at:
- **Email:** niko2357@example.com
- **LinkedIn:** [linkedin.com/in/niko2357](https://linkedin.com/in/niko2357)
- **Twitter:** [twitter.com/niko2357](https://twitter.com/niko2357)

Happy flying! ✈️