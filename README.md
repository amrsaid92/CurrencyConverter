# Project Overview

This project follows the Onion Architecture, which I have implemented as a base structure. This architecture serves as a foundation for most of my projects, ensuring scalability, maintainability, and separation of concerns.

## Project Descriptions

Below is a detailed description of each project included in this repository.

## Core Layer

The Core Layer contains essential components that define the foundation of the application. It includes the following projects:

- **CurrencyConverter.Core**: Contains core utilities and shared classes that can be reused across multiple projects.
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

### Calling the Frankfurt API
- Used [Refit](https://github.com/reactiveui/refit), a REST client library for .NET, to simplify API calls by generating strongly typed HTTP clients.
- To exclude specific currencies, they are listed in the application settings. This approach can be extended to store exclusions in the database for runtime configuration.

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

### Health Checks & API Monitoring
- Implemented a **Health Check API** to monitor API availability, integrated with Seq.
- Added a **dashboard** to visualize API call statistics and availability using collected logs.
