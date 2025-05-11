# CropDeal.API

## Overview

`CropDeal.API` is a robust backend system designed for modern agricultural trading systems. Built using **C#** for its core functionalities and **CSS** for styling, the API serves as a bridge between farmers, dealers, and administrators. This repository emphasizes modularity, scalability, and security in its design.

## Features

- **Authentication and Authorization**: Secure user registration, login, and role-based access control.
- **User Management**: Comprehensive user profiles with roles like Farmer, Dealer, and Admin.
- **Transaction Handling**: Secure and efficient transaction processing.
- **Review System**: Farmers and dealers can exchange feedback through reviews.
- **Crop Management**: Administer crop data and listings.
- **Bank and Address Management**: Securing user banking and address details.
- **Reporting**: Admins can generate detailed reports.

## Getting Started

### Prerequisites

Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- A running SQL database instance.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Moon-Elf/CropDeal.API.git
   ```
2. Navigate to the project directory:
   ```bash
   cd CropDeal.API
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the project:
   ```bash
   dotnet run
   ```

## Controllers and Endpoints

### 1. **AuthController**
Handles user authentication and authorization.
- `POST /api/auth/register` - Register a new user.
- `POST /api/auth/login` - Login and generate JWT.
- `GET /api/auth/authorized/farmer` - Farmer-specific endpoint.
- `GET /api/auth/authorized/dealer` - Dealer-specific endpoint.

### 2. **ReviewController**
Manages user reviews.
- `GET /api/review/{id}` - Fetch a review by ID.
- `GET /api/review` - Get all reviews.
- `GET /api/review/farmer/{farmerId}` - Get reviews for a farmer.
- `GET /api/review/dealer/{dealerId}` - Get reviews for a dealer.
- `POST /api/review` - Create a new review.

### 3. **UsersController**
Manages user data.
- `GET /api/users` - Fetch all users.
- `GET /api/users/{id}` - Get user details by ID.

### 4. **TransactionController**
Handles transactions between users.
- `GET /api/transaction` - Get all transactions.
- `GET /api/transaction/my` - Get transactions of the logged-in user.
- `POST /api/transaction` - Create a new transaction.

### 5. **AddressController**
Manages user addresses.
- `GET /api/address` - Get all addresses for the logged-in user.
- `POST /api/address` - Add a new address.
- `PUT /api/address` - Update an existing address.

### 6. **CropController**
Handles crop data.
- `GET /api/crop` - Get all crops.
- `GET /api/crop/{id}` - Get crop details by ID.
- `POST /api/crop` - Add a new crop (Admin-only).

### 7. **CropListingController**
Manages crop listings.
- `GET /api/croplisting` - Get all crop listings.
- `POST /api/croplisting` - Create a new listing.

### 8. **ProfileController**
Handles user profiles.
- `GET /api/profile` - Fetch the logged-in user's profile.
- `PUT /api/profile` - Update the logged-in user's profile.

### 9. **ReportController**
Generates reports (Admin-only).
- `POST /api/report` - Create a new report.
- `GET /api/report` - Fetch all reports.

### 10. **BankAccountController**
Secures user bank account details.
- `GET /api/bankaccount` - Get all bank accounts for the logged-in user.
- `POST /api/bankaccount` - Add a new bank account.

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch for your changes:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to your branch:
   ```bash
   git push origin feature-name
   ```
5. Open a Pull Request on the [repository](https://github.com/Moon-Elf/CropDeal.API).

## License

This project does not currently specify a license. Consider adding one to clarify usage rights.

## Contact

For questions or inquiries, reach out to the repository owner via [GitHub Profile](https://github.com/Moon-Elf).

---

This README was generated with ❤️ by [Moon-Elf](https://github.com/Moon-Elf).
