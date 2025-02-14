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
