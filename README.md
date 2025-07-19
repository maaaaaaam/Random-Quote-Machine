# Fullstack Random Quote Machine
A fullstack random quote machine that:

* Fetches quotes from a **Microsoft SQL Server** database
* Allows quote import via a **`.csv` file**
>The app is run on Docker containers

## Launching:

```bash
docker compose up --build
```
Wait until the containers are ready, open `localhost:80` in your browser and that's it!

---

Importing quotes via the frontend UI requires a `.csv` file in the format `"Quote text","Author"` without table headers. There is `5quotesToAdd.csv` in the **root folder**. This file exists to test the importing via the frontend UI.

The reinitialization uses `backend/10InitialQuotesForReinit.csv` to import into the table after **clearing it**.

---
>**TO DO: rewrite the backend in Java**