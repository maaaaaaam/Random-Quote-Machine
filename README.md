# Fullstack Random Quote Machine

- Run backend: dotnet run --urls http://localhost:5142
- Run frontend (fetching from the port 5142 is hardcoded): npm run dev

Import quotes.csv file into a Microsoft SQL Server table "Quotes" with an autoincrementing id column, connect to your database in ConnectionStrings.DefaultConnection in appsettings.json.