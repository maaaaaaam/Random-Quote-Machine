# Fullstack Random Quote Machine
Random quote machine fetching quotes from a MS SQL Server database and able to import quotes via a .csv file.

10initialquotes.csv in the root folder is the file with quotes for the database. This file has "Text" and "Author" table headers. Import it directly into a Microsoft SQL Server table "Quotes" with an auto-incrementing id column, connect to your database in ConnectionStrings.DefaultConnection in appsettings.json.

Launching:
- Run backend (the terminal in ./backend): 
    - dotnet run

- Run frontend (the terminal in ./frontend):
    - npm i
    - npm run dev

By default, the app uses ports 5142 and 5173 for backend and frontend respectively. You can change the ports in the .env file, located in the root folder. When running "npm run dev", this file is copied into ./frontend in order to use the ports in the fronted code.

Importing quotes via the frontend UI requires a .csv file in the format "Quote text","Author" without table headers. There is 5quotesToAdd.csv in the root folder. This file can be used for the import via the frontend UI.

The reinitialization uses backend/10InitialQuotesForReinit.csv to import into the table after clearing it.