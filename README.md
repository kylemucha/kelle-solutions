
# Razor Pages ASP.NET Core with Entity Framework Core

This project is a simple Razor Pages web application built with ASP.NET Core and Entity Framework Core. The application demonstrates basic CRUD (Create, Read, Update, Delete) functionality using a database (SQL Server).

## Team Members
Nancy Zhu
Steven Cao

## Features

- Razor Pages with ASP.NET Core
- Entity Framework Core for data access
- SQL Server database for persistence
- Dependency Injection for `DbContext`
- CRUD operations for managing student data

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-2016-express-localdb)
- A text editor or an IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

Follow these steps to get the application up and running:

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/razor-pages-ef-core.git
cd razor-pages-ef-core
```

### 2. Install Dependencies

Install the required NuGet packages if they are not installed:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 3. Set Up the Database Connection

Update the `appsettings.json` file to point to your SQL Server or LocalDB:

```json
{
  "ConnectionStrings": {
    "SchoolContext": "Server=(localdb)\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Create the Database

Apply the initial Entity Framework migration to create the database schema:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

Use the following command to run the application locally:

```bash
dotnet run
```

The application will start on `https://localhost:5001` or `http://localhost:5000`. Navigate to `/Students` to view the CRUD interface for managing student data.

## Project Structure

- `Pages/Students`: Razor Pages for creating, reading, updating, and deleting students.
- `Models/Student.cs`: The `Student` entity class.
- `Data/SchoolContext.cs`: The database context class for managing the database connection and querying the `Student` model.

## Technologies Used

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server / LocalDB
- Bootstrap (for simple page styling)

## Screenshots

### List of Students
![Students List](screenshots/students-list.png)

### Create Student
![Create Student](screenshots/create-student.png)

## How to Contribute

If you'd like to contribute to the project:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -m 'Add my feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
