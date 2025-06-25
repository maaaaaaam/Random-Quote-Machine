# Fullstack Random Quote Machine

- Run backend: dotnet run --urls http://localhost:5142
- Run frontend (fetching from the port 5142 is hardcoded): npm run dev

Import quotes.csv file into a Microsoft SQL Server table "Quotes" with an auto-incrementing id column, connect to your database in ConnectionStrings.DefaultConnection in appsettings.json.

There is the backend part of an API for importing quotes to the table. Post quotesToAdd.csv (located in "backend" folder) to http://localhost:5142/api/quotes to add 5 quotes to the default 10 quotes from quotes.csv. The API accepts .csv files in the format "Text of the quote","Author" without the table headers. The frontend UI for the API is under development.