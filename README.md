# Enterprise Console Application: Library Management System

## 1\. Introduction

This document outlines the architecture and operational procedures for a console-based Library Management System. Developed using C\# on the .NET platform, this application provides a robust, text-based interface for core library operations. The system leverages Entity Framework (EF) Core for all data persistence, ensuring a clean separation between the application's business logic and the underlying SQL Server database.

The design prioritizes a layered architecture, modularity, and maintainability, serving as a foundational example of best practices in modern .NET console application development.

## 2\. System Capabilities

The application's functionality is segmented into three primary domains: user account management, book inventory control, and circulation services.

### 2.1. User & Authentication Services

  * **User Authentication**: Implements a secure user authentication mechanism for system access.
  * **Account Registration**: Provides a self-service registration module for new users, complete with data validation protocols.
  * **Profile Management**: Registered users have the capability to modify their account details, including full name, email, and password, protected by current password verification.

### 2.2. Book & Loan Management

  * **Inventory Inquiry**: Users can retrieve and display a complete list of all books within the library's catalog, including their current availability status.
  * **Circulation Services**: The system manages the borrowing and returning of books. The availability status of each book is updated in real-time to prevent transactional conflicts.
  * **Personal Loan History**: Users can view a manifest of all books currently borrowed under their account.
  * **Catalog Search**: A search utility allows users to query the book catalog by title or author keywords.

### 2.3. Administrative Functions

  * **User Roster**: The system includes functionality to display a comprehensive list of all registered users.

## 3\. Technical Architecture

The system is constructed upon a curated stack of Microsoft technologies, ensuring stability and performance.

  * **Runtime**: .NET
  * **Primary Language**: C\#
  * **Framework**: Console Application
  * **Data Access Layer**: Entity Framework Core
  * **Database Provider**: Microsoft SQL Server (configured for LocalDB)

## 4\. Environment Setup and Deployment

To compile and execute this application, the following prerequisites and procedures must be followed.

### 4.1. Prerequisites

  * .NET SDK (6.0 or later)
  * Microsoft Visual Studio IDE (recommended) or a compatible code editor.
  * SQL Server Express LocalDB, which is typically included with Visual Studio installations.

### 4.2. Build & Execution Protocol

1.  **Repository Acquisition**: Clone the source code repository to a local machine.
2.  **Database Configuration**: The database connection string is defined within `ApplicationDBContext.cs`. The default configuration targets a LocalDB instance named `EFLibraryDB`. Adjust this string if an alternative SQL Server instance is required.
3.  **Database Schema Migration**: Execute the following EF Core commands from the project's root directory in a terminal or the Package Manager Console to create and apply the necessary database schema.
    ```shell
    # This command generates the initial migration scripts based on the models.
    dotnet ef migrations add InitialCreate

    # This command applies the generated migration to the target database.
    dotnet ef database update
    ```
4.  **Application Execution**: Run the application using the .NET CLI or directly from your IDE.
    ```shell
    dotnet run
    ```

## 5\. Architectural Overview

The project adheres to the principle of Separation of Concerns through a well-defined, layered architecture.

  * **`LibrarySystemWithEF.Models`**: This namespace constitutes the data layer, containing the Plain Old CLR Object (POCO) entity classes (`User`, `Book`, `Borrowing`). These classes map directly to the database tables and define the relational schema.

  * **`LibrarySystemWithEF.ApplicationDBContext`**: The EF Core `DbContext` class resides here. It acts as the primary conduit for all database communication, managing the session, transactions, and data translation between objects and relational data.

  * **`LibrarySystemWithEF.Services`**: This layer encapsulates the core business logic.

      * **Interfaces (`IUserService`, `IBookService`, `IBorrowingService`)**: These define the service contracts, establishing a clear API for the application's business operations.
      * **Implementations (`UserService`, `BookService`, `BorrowingService`)**: These classes contain the concrete implementation of the business logic, interacting directly with the `ApplicationDBContext` to perform data manipulation.

  * **`LibrarySystemWithEF.Views`**: The presentation layer is managed entirely by the `ConsoleView` class. It is a static class responsible for rendering all output to the console and capturing all user input, ensuring that UI logic is completely decoupled from the application's core functionality.

  * **`LibrarySystemWithEF.MenuController`**: This class acts as the central controller, orchestrating the application flow. It responds to user input by invoking the appropriate services and directing the `ConsoleView` to display the correct information.

## 6\. Operational Flow

Upon execution, the application enters a state-driven loop initiated by the `MenuController`.

1.  **Initial State**: The user is presented with an initial screen offering options to log in, register, or exit the application.
2.  **Authentication**: A valid session is established upon successful login or registration. Failure to authenticate returns the user to the initial state.
3.  **Main Application Loop**: Once authenticated, the user gains access to the main menu, which presents the system's core functionalities. The application remains in this state, processing user commands, until the exit option is selected.
4.  **Session Termination**: The application terminates upon user request from the main menu or the initial screen.


## 7\. Roadmap & Practical Enhancements

While the current implementation works well for its intended purpose, several simple enhancements can make the system more user-friendly and robust:

* **Enhance Data Validation**:
   * Implement more sophisticated validation logic within the service layer to ensure data integrity beyond basic format checks.
* **Basic Role Concept (Optional)**:
  * Add a simple `Role` property for users (e.g., "Admin" or "Member").
  * Use it to control which menu options are visible, without adding complex access control.
* **Enhanced Search**:
  * Allow searching by book title or author, showing partial matches as well.
