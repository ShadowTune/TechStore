# TechStore - E-Commerce Web Application

TechStore is a full-featured e-commerce web application built with ASP.NET. It provides product listings, shopping cart functionality, secure payments, user authentication, and order tracking.

## Features

- **Product Listings with Categories & Filters**: Browse products in various categories, and filter based on price, rating, etc.
- **User Authentication**: Secure login and registration system for users to access their accounts.
- **Payment Gateway Integration**: Process payments securely using [Stripe/PayPal].
- **Order Placement & Delivery Tracking**: Users can place orders and track delivery status in real-time.
- **Cart & Wishlist Functionality**: Add products to the cart or wishlist for future purchases.
- **Admin Dashboard**: Admins can manage products, view orders, and perform administrative tasks from a dedicated dashboard.

## Tech Stack

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: ASP.NET (C#)
- **Database**: SQL Server
- **Payment Integration**: [Stripe/PayPal] (Specify which one you're using)
- **Development IDE**: Visual Studio

## Installation Guide

### 1. Clone the Repository

Clone the repository to your local machine.

```bash
git clone https://github.com/yourusername/TechStore.git
cd TechStore


### 2. Open the Project in Visual Studio

- Launch **Visual Studio** on your system.
- Open the `.sln` (Solution) file located in the root directory of the project.
  - The `.sln` file is named `TechStore.sln` or something similar based on your project configuration.
  - You can do this by selecting `File` > `Open` > `Project/Solution` and then navigating to the directory where the project is located. Select the `.sln` file to open the entire project in Visual Studio.

Once the project is open in Visual Studio, you should see the solution and all the project files in the Solution Explorer on the right side of the screen.


### 3. Restore NuGet Packages

Before you can run the project, you need to restore the necessary NuGet packages that are required by the project.

- Open a terminal or command prompt in the root directory of your project (where the `.sln` file is located).
- Run the following command to restore all the NuGet packages specified in the project:

```bash
dotnet restore


### 4. Configure the Database

To configure the database, you'll need to set up the connection string in the project.

- Open the `appsettings.json` file located in the root directory of the project.
- Find the section named `ConnectionStrings` and update the **connection string** to point to your local or remote SQL Server database.

Example connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=yourserver;Database=TechStoreDB;Trusted_Connection=True;"
  }
}


### 5. Run Database Migrations

To set up the database schema, you need to run the migrations to create the necessary tables in your SQL Server database.

- Open a terminal or command prompt in the root directory of your project (where the `.sln` file is located).
- Run the following command to apply the database migrations:

```bash
dotnet ef database update


