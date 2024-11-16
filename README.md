# Talabat-API

## Description

The **Talabat-API** is a back-end project designed to provide a comprehensive API for an online ordering system. It is built with **ASP.NET Core** and follows best practices for **clean architecture**, incorporating design patterns such as **Repository**, **Unit of Work**, **CQRS**, and **Dependency Injection**. This API supports user registration, authentication, order processing, and payment integration.

---

## Features

- **User Authentication & Authorization**: 
  - Implemented using **ASP.NET Identity**.
  - Includes JWT token-based authentication.
  - Admin and User roles with access control.

- **Order Management**: 
  - Ability to place, update, and view orders.
  - Order status management (Pending, Confirmed, Shipped, etc.).
  
- **Product Catalog**: 
  - Product management (CRUD operations).
  - Categories and filtering by product attributes.

- **Payment Integration**: 
  - Integration with **Stripe** for payment processing.

- **Notifications**: 
  - Real-time order updates using **SignalR**.

- **Logging and Error Handling**: 
  - Detailed logging with **Serilog** and exception handling.

- **Clean Architecture**: 
  - Follows the principles of **Domain-Driven Design**.
  - Separates concerns into distinct layers: **Domain**, **Application**, **Infrastructure**, and **API**.

- **Unit Testing**: 
  - Extensive unit tests covering business logic and API endpoints.
  - Mocked services for external dependencies.

---

## Technologies

- **.NET Core 8**: Framework for building modern, scalable APIs.
- **ASP.NET Identity**: For user authentication and authorization.
- **JWT**: For secure authentication via tokens.
- **Entity Framework Core**: ORM for database access.
- **SignalR**: For real-time notifications.
- **Stripe**: For payment gateway integration.
- **FluentValidation**: For input validation.
- **Swagger**: For API documentation and testing.
- **Serilog**: For logging.
- **AutoMapper**: For object mapping between DTOs and entities.
- **MediatR**: For CQRS pattern implementation.

---

## Design Patterns

- **Repository Pattern**: Provides a clean separation of concerns between the business logic and data access layer.
- **Unit of Work Pattern**: Manages transactions and ensures that all changes to data are committed or rolled back together.
- **CQRS (Command Query Responsibility Segregation)**: Separates reading and writing operations into different models, improving performance and scalability.
- **Dependency Injection**: Used for dependency management and loose coupling between classes.

---

## Endpoints

### Account

- **POST /api/Account/Login**: Login a user and generate a JWT token.
- **POST /api/Account/Register**: Register a new user.

### Basket

- **GET /api/Basket/{Id}**: Retrieve the basket by ID.
- **DELETE /api/Basket/{Id}**: Delete the basket by ID.
- **POST /api/Basket**: Create a new basket.

### Buggy

- **GET /api/Buggy/NotFound**: Simulate a "Not Found" error.
- **GET /api/Buggy/ServerError**: Simulate a "Server Error".
- **GET /api/Buggy/BadRequest**: Simulate a "Bad Request" error.
- **GET /api/Buggy/BadRequest/{Id}**: Simulate a "Bad Request" error with an ID parameter.

### Orders

- **POST /api/Orders**: Place a new order.
- **GET /api/Orders/Orders**: Retrieve all orders.
- **GET /api/Orders/{Id}**: Retrieve an order by ID.
- **GET /api/Orders/Delivery**: Retrieve orders for delivery.

### Payment

- **POST /api/Payment**: Process a payment.
- **POST /api/Payment/Webhook**: Handle payment webhook notifications.

### Product

- **GET /api/Product/Products**: Retrieve all products.
- **GET /api/Product/{Id}**: Retrieve a product by ID.
- **GET /api/Product/Categories**: Retrieve product categories.
- **GET /api/Product/Brands**: Retrieve product brands.

---

## Installation

### Prerequisites

- .NET 8 SDK
- SQL Server or any relational database
- Stripe API credentials

### Steps to Run Locally

1. Clone the repository:
   ```bash
   git clone https://github.com/SharaawyMohamed/Talabat-API.git
