# Project Overview

This project follows the Onion Architecture, which I have implemented as a base structure. This architecture serves as a foundation for most of my projects, ensuring scalability, maintainability, and separation of concerns.

## Project Descriptions

Below is a detailed description of each project included in this repository.

## Core Layer

The Core Layer contains essential components that define the foundation of the application. It includes the following projects:

- **CurrencyConverter.Core**: Contains core utilities and shared classes that can be reused across multiple projects.
- **CurrencyConverter.DBChanges**: Contains DB schema for (Tables, Views, SPs, etc...). So that it can be deployed using CI/CD.
- **CurrencyConverter.Entities**: Defines the entity classes that represent the database models.
- **CurrencyConverter.RepositoryInterfaces**: Declares the repository interfaces to interact with the database, ensuring database-agnostic data access.
- **CurrencyConverter.Resources**: Manages localization resources, including string translations for different languages.

## Application Layer

The Application Layer is responsible for handling business logic and data transfer between the Core and Presentation layers. It includes the following projects:

- **CurrencyConverter.DomainEntities**: Contains all Data Transfer Objects (DTOs) used in the Presentation Layer.
- **CurrencyConverter.ServiceInterfaces**: Defines the interfaces for business logic services.
- **CurrencyConverter.Services**: Implements business logic and interacts with repositories to communicate with the database.

## Presentation Layer

The Presentation Layer exposes the application's functionality through a well-structured API. It includes the following project:

- **CurrencyConverter.API**: The API project that defines and exposes all endpoints for client applications.

## Infrastructure Layer

The Infrastructure Layer provides implementations for data access and dependency injection. It includes the following projects:

- **CurrencyConverter.Bootstrapper**: Configures and initializes dependency injection for all application layers.
- **CurrencyConverter.Repositories**: Implements repository interfaces to facilitate communication with an SQL database.

## Design Patterns

This project uses several design patterns to improve structure, flexibility, and maintainability:

- **Repository Pattern**:  
  Separates data access logic from business logic, making database interactions more manageable.

- **Unit of Work**:  
  Ensures multiple database operations are executed together, preventing data inconsistencies.

- **Dependency Injection (DI)**:  
  Makes the code more modular and testable by injecting dependencies instead of hardcoding them.

- **Strategy Pattern**:  
  Allows switching between different business logic implementations at runtime.

- **Singleton Pattern**:  
  Ensures a class has only one instance throughout the application, useful for configurations and logging.

## Task Description and Assumptions

## Task Setup

To set up and run this project, ensure the following prerequisites are met:

- **Development Environment**:  
  - Visual Studio **2022**  
  - .NET **8**  

- **Database**:  
  - A **database backup** is included in the project files for easy restoration.

- **Logging**:  
  - **Seq (v2024.3)** is installed and running on **port 5341** for structured logging.

Ensure all dependencies are installed and configured before running the project.

### Calling the Frankfurt API
- Used [Refit](https://github.com/reactiveui/refit), a REST client library for .NET, to simplify API calls by generating strongly typed HTTP clients.
- To exclude specific currencies, they are listed in the application settings. This approach can be extended to store exclusions in the database for runtime configuration.

### API Versioning
- Used [Asp.Versioning.Http](https://github.com/dotnet/aspnet-api-versioning) to enable API versioning.
- Supports versioning through both **URL** and **Header**.
- Implemented two versions of the **"Latest" API**:
  - **V1**: Returns a result object with result code `2` when an excluded currency is entered.
  - **V2**: Returns a **Bad Request** response when an excluded currency is entered, as per the new requirement.

### Caching Strategy
- Implemented **Output Caching** to improve response time.
- Other caching options such as **In-Memory Cache** and **Distributed Cache** can be used, especially when multiple servers are involved in a load-balanced environment.
- Starting from **.NET 9**, **Hybrid Cache** is available, allowing automatic switching between in-memory and distributed cache based on availability.

### Resilience (Retry & Circuit Breaker)
- Used **Microsoft.Extensions.Http.Polly** to handle retry policies and circuit breaker functionality, improving API request stability.

### Logging & Monitoring
- Implemented logging using **Serilog** with **Seq** for structured logging (assumed to be hosted at `http://localhost:5341`, version `2024.3`).
- Logged each API request, including user correlation details (IP & Email if the user is authenticated).
- Below are screenshots showcasing logs in Seq.
![Image](https://github.com/user-attachments/assets/9f2a549d-be72-4b58-bda5-930284150db3)
### Health Checks & API Monitoring
- Implemented a **Health Check API** to monitor API availability, integrated with Seq.
![Image](https://github.com/user-attachments/assets/998769e0-fe89-433f-8bdb-d650aa7b54a3)
- Added a **dashboard** to visualize API call statistics and availability using collected logs.
![Image](https://github.com/user-attachments/assets/dd7ba9cf-a183-496f-8ed7-d8d3626fbdc8)

## JWT Authentication

JWT (JSON Web Token) is used for both authentication and authorization. The implementation assumes a database with a `Users` table and includes an `AccountController` to manage user registration and token generation.

### Key Features:
- **User Registration**:  
  - Passwords are securely **hashed** before being stored in the database.
  - Users can be assigned roles (`Admin`, `User`).

- **Role-Based Authorization**:  
  - Roles are stored as a **list of strings**, allowing multiple roles per user.
  - API access is controlled based on user roles:
    - **Latest API V1**: Accessible by both `Admin` and `User`.
    - **Latest API V2**: Accessible only by `User`.
    - **Convert API**: Publicly accessible (no authentication required).
    - **GetRange API**: Restricted to `Admin` users only.

- **JWT Configuration**:  
  - All JWT settings (e.g., secret key, expiration time) are stored in the `AppSettings` file.

## Testing

Both **Unit Tests** and **Integration Tests** were implemented to ensure the reliability of the application.

### Unit Testing
- Used **Moq** to mock dependency injection for the **Currency Service**.
- Tested the **Latest API**, verifying the differences between **V1** and **V2**.

### Integration Testing
- Covered different scenarios for the **Convert API**.
- Added a test case for an **unauthorized request**.

While many additional test cases can be implemented, the current tests demonstrate the core testing approach.

## Deployment & CI/CD

The project follows a **CI/CD (Continuous Integration & Continuous Deployment)** workflow using **Azure DevOps**. Below are screenshots from a test project where CI/CD pipelines and releases were implemented.

### CI/CD Pipeline:
- Configured tasks to **build** and **package** both:
  - The **Web API project**.
  - The **SQL Database change project**.

### Release Process:
- Deployment tasks are structured as follows:
  1. **Database Changes Deployment** – Applied database migrations before deploying the application.
  2. **Web API Deployment** – Deployed the application using **Deployment Groups** to predefined **Staging** and **Production** servers.
![Image](https://github.com/user-attachments/assets/b52e0fed-6ddc-442d-b372-facfdc7197bb)
### Deployment Stages:
- Implemented **two deployment stages**:
  - **Staging**: For testing in a controlled environment.
  - **Production**: For live deployment.
![Image](https://github.com/user-attachments/assets/457bf5e0-63c1-4162-a586-d189ad577dbf)
### Horizontal Scaling:
- Configured **multiple servers per deployment group**, allowing the release pipeline to deploy changes across all instances.
- This setup enables seamless **scaling** by adding more nodes when needed.
![Image](https://github.com/user-attachments/assets/96916924-2807-4bb3-be41-481483cc9281)
### Approval Process:
- **Pre-approval** is required for each stage to ensure controlled and secure deployments.
![Image](https://github.com/user-attachments/assets/839b5f01-fa7b-478d-a324-2bc049ef4825)

