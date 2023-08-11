
## SQL Server, Migrations & Docker

In order to have a working Database, using Docker (assuming that you already have the Docker Environment working), you should run the following command:

```
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=ab123CD*" -p 1433:1433 --name sql1 --hostname sql1n -d mcr.microsoft.com/mssql/server:2022-latest
```

After your database is online and kicking :grin:, you can populate it, either with the SQL Script in the folder Scripts (inside the project *Injection.Data*) or using migrations, with the command:

```
dotnet ef database update ~/YOUR_PATH/Injection/Injection.Data
```

***Important note***: This command should run on the folder 

```
~/YOUR_PATH/Injection/Injection.API
```

Also, if you run this command, tables, initial values, and also the Store Procedure, are responsible for creating new orders.


