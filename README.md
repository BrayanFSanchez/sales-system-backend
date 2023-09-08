## Project Name
Welcome to my ASP.NET Core Web API backend project called sales-system-backend.

## Description
It is a RESTful Web API in ASP.NET Core (net7.0) with a SQL Server database, to effectively consume the services in a web application.

## Installation
1. Clone the repository: [https://github.com/BrayanFSanchez/asp.net-core-7-villas-crud-backend.git](https://github.com/BrayanFSanchez/sales-system-backend.git)
2. Make sure you have the .NET Core SDK installed on your machine.
3. Download and install SQL Server LocalDB.

## Configuration
1. Open the appsettings.json file located at the root of the project and add the connection string to the database.
2. Run the migrations to create the database.

* Endpoint: DELETE /api/villas/{id}
* Description: This endpoint allows you to remove a villa from the database based on its unique identifier (id). After successful deletion, the villa's information will no longer be available in the system.

## Usage
To start the server, run the following command:
dotnet run

## Project structure
The folder structure you mentioned (API, BLL, DAL, DTO, IOC, Model, Utility).

## APIs and endpoints
APIs represent sets of endpoints that provide functionality and enable communication with server. Each API focuses on a specific domain of the application. Each API has designated endpoints, which are the paths through which that functionality is accessed.

![sales-system-api](https://github.com/BrayanFSanchez/sales-system-backend/assets/49698030/ba5f78f5-00f8-4824-929e-eeaec8265370)

### Category
* Endpoint: GET /api/Category/List
* Description: This endpoint retrieves a list of all the categories stored in the database.

### DashBoard
* Endpoint: GET /api/DashBoard/Summary
* Description: This endpoint retrieves a list of all the information stored in the database regarding a specific authenticated user.

### Menu
* Endpoint: GET /api/Menu/List
* Description: This endpoint retrieves a list of all the information stored in the database regarding a specific menu.

### Products
* Endpoint: GET /api/Products/List
* Description: This endpoint retrieves a list of all the products stored in the database.

* Endpoint: POST /api/Product/Save
* Description: This endpoint allows you to create a new product entry in the database.

* Endpoint: PUT /api/Product/Edit
* Description: This endpoint enables you to update the information of an existing product.

* Endpoint: DELETE /api/Product/Delete/{id}
* Description: This endpoint allows you to remove a product from the database based on its unique identifier (id).

### Role
* Endpoint: GET /api/Role/List
* Description: This endpoint retrieves a list of all the information stored in the database regarding Rol.

### Sales
* Endpoint: POST /api/Sale/Register
* Description: This endpoint allows you to create a new Sale entry in the database.

* Endpoint: GET /api/Sale/Report
* Description: This endpoint retrieves a sale stored in the database according to a date.

* Endpoint: GET /api/Sale/Record
* Description: This endpoint retrieves a sale stored in the database according to a filter.

### User
* Endpoint: POST /api/User/Save
* Description: This endpoint allows you to create a new User entry in the database.

* Endpoint: GET /api/User/List
* Description: This endpoint retrieves a list of all the users stored in the database.

* Endpoint: PUT /api/User/Edit
* Description: This endpoint enables you to update the information of an existing user.

* Endpoint: DELETE /api/User/Delete/{id}
* Description: This endpoint allows you to remove a user from the database based on its unique identifier (id).

## Packages
* AutoMapper (v12.0.1): AutoMapper is a library that simplifies object-to-object mapping in an ASP.NET Core application. It automates the process of converting data between model classes and entities, saving time and reducing repetitive code.

* AutoMapper.Extensions.Microsoft.DependencyInjection (v12.0.1): This package is an extension for AutoMapper that facilitates its integration with the ASP.NET Core dependency injection system. It streamlines the configuration of AutoMapper in the application and allows its use throughout the lifetime of dependencies.

* Microsoft.EntityFrameworkCore.SqlServer (v7.0.3): Microsoft.EntityFrameworkCore.SqlServer is the database provider for SQL Server in Entity Framework Core. This package enables you to use Entity Framework Core to interact with a SQL Server database in your ASP.NET Core application.

* Microsoft.EntityFrameworkCore.Tools (v7.0.3): Microsoft.EntityFrameworkCore.Tools provides additional tools for working with Entity Framework Core, such as database migrations. It simplifies the creation, application, and rollback of migrations to keep the database schema in sync with the application's data model.
