Library Management System (.NET Core)

Technology Stack

Language: C#

Framework: ASP.NET Core Web API

Database: Entity Framework Core with SQLite

Authentication: ASP.NET Identity with JWT

Tools: Visual Studio / VS Code, Postman

Version Control: GitHub

Setup Instructions

1.Clone the repository
git clone - https://github.com/uminduchethiya/LibraryManagementSystem.git
cd library-management-system

2.Restore NuGet packages
dotnet restore

3.Required NuGet packages(If not already)
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore

4.Configure Database & JWT
"ConnectionStrings": {
  "DefaultConnection": "Data Source=library.db"
},
"Jwt": {
  "Key": "YourSuperSecretKeyHere",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience"
}

5.Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

6.Run the Application
dotnet run




