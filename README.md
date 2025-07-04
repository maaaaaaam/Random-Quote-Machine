# Fullstack Random Quote Machine
A fullstack random quote machine that:

* Fetches quotes from a **Microsoft SQL Server** database
* Allows quote import via a **`.csv` file**
* Has a frontend and backend with **customizable ports**
>The app is run on `localhost`
---

`10initialquotes.csv` in the **root folder** is the file with quotes for the database. This file has `Text` and `Author` table headers. 
1. Import it directly into a **Microsoft SQL Server table** entitled "`Quotes`" with an auto-incrementing id column
2. Connect to your database in `ConnectionStrings.DefaultConnection` in `appsettings.json`

## Launching:
>Don't forget to connect to the DB as described above!
### Run backend 
```bash
cd backend
dotnet run
```
### Run frontend:
```bash
cd frontend
npm i
npm run dev
```
---
By default, the app uses `localhost` ports `5142` and `5173` for backend and frontend respectively. You can change the ports in the `.env` file, located in the **root folder**. When running `npm run dev`, this file is **automatically copied** into `./frontend` in order to use the ports in the frontend code.

Importing quotes via the frontend UI requires a `.csv` file in the format `"Quote text","Author"` without table headers. There is `5quotesToAdd.csv` in the **root folder**. This file exists to test the importing via the frontend UI.

The reinitialization uses `backend/10InitialQuotesForReinit.csv` to import into the table after **clearing it**.

---
> To-do: working on Docker now