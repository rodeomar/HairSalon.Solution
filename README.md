# Hair Salon Web Application

This is a web application for managing a hair salon. It allows users to view, add, and search for stylists and clients in the salon.

## Features

- View a list of all stylists and their specialties.
- View a list of all clients and their assigned stylist.
- Add a new stylist to the salon.
- Add a new client to the salon and assign them to a stylist.
- Search for stylists and clients by name.
- and more...

## Technologies Used

- ASP.NET Core MVC: The web application is built using ASP.NET Core MVC framework to handle HTTP requests and responses.
- C#: The backend logic and data processing are implemented using C# programming language.
- MySQL: The application uses MySQL database to store and manage stylist and client data.
- Entity Framework Core: Entity Framework Core is used for object-relational mapping (ORM) to interact with the MySQL database.
- Bootstrap: The frontend is designed using Bootstrap CSS framework to ensure responsiveness and a clean user interface.

## Getting Started

1. Clone the repository to your local machine:
```
git clone https://github.com/rodeomar/HairSalon.Solution
```

2. Create a MySQL database and tables using the provided SQL scripts in the `HairSalon` Folder.

3. Update the connection string in the `appsettings.json` file with your MySQL database credentials.
```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=db_name;uid=username;pwd=password;"
  }
```
4. Build and run the application: `dotnet run` or `dotnet watch run` to run in watcher mode.

5. Open your web browser and visit `https://localhost:5000` to access the application.

## Usage

- When you open the application, you will see a list of all stylists and their specialties.
- You can click on view details button to see details for stylist, including a list of clients assigned to them.
- To add a new stylist, click the "Add New Stylist" button and fill in the required information in the popup modal.
- To add a new client, click the "Add New Client" button on the client list page and fill in the required information, including selecting a stylist from the dropdown menu.
- To search for a specific stylist or client, use the search bar and enter the name and click search. The list will update with the search results.



