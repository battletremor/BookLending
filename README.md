# BookLending

## Overview

This repository contains project built with .NET 8, please make sure the runtime/sdk is installed which can be installed from the official microsoft site:

**Book Lending API**: A web API for managing/listing books with user authentication.

## Technologies Used

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core)
- [JWT Authentication Work in-prorgess](https://code-maze.com/dotnetcore-secure-microservices-jwt-ocelot/) 

## Project Structure

- **DTO**: [Class library]
  - Contains the source code and configuration for the Databse Object Relation Mapping along with migrations to be applied to construct the database from scratch.
    
## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/battletremor/BookLending.git
   cd BookLending
   ```
2. Build and run the projects:
  - Navigate to project directory (Booklending/) and run:
    ```bash
    dotnet build
    dotnet run
    ```
3. Open your browser and navigate to the API endpoints:
   
  - BookLending API: http://localhost:7296/swagger

## Configuration

- Each project may have its own configuration files. Check the appsettings.json or respective configuration files for customization. The Connection string is required to modified to connect and interact with the SQL database.

> [!CAUTION]
> This project uses http scheme, additionally it has support for https but the ports may vary. Please check launchSettings.json of each project to know more about.

> [!TIP]
> The webapi project has swaggeeUI enabled which can be disabled by commenting the launchUrl key-value pair in launchSettings.json
