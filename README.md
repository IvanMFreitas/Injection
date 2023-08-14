
# Sample .NET API 

This is a generic API, created, using some technologies and design patterns




## Technology Stack

**.NET 7**

**Entity Framework CORE**

**Microsoft SQL Server w/ Docker**

**JWT Authentication**

**xUnit for testing**

### Also used as Pattern:

**Dependency Injection**

**Repository Pattern and UnitOfWork**
## Models

There are 3 models on the Database that you should know:

**Person** with fields:

- Id (Guid)
- Name (String)
- Email (String)
- IsAdmin (Boolean)
- CreatedAt (DateTime)

**Product** with fields:

- Id (Guid)
- Name (String)
- Price (Decimal)
- CreatedAt (DateTime)

**Order** with fields:

- Id (Guid)
- PersonId (Guid - FK)
- ProductId (Guid - FK)
- Qty (Integer)
- Total (Decimal)
- CreatedAt (DateTime)



## API Documentation

#### Returns a Product by its Id

```http
  GET /api/product/{id}
```

#### Returns a Person by its Id

```http
  GET /api/person/{id}
```

#### Returns an Order by its Id

```http
  GET /api/order/{id}
```

#### Creates a valid JWT Token by User e-mail

***Use this endpoint to create a valid Token to submit into Create Order***

*For sample purpouses, you can use the email 'person1@api.com' for admin access, and 'person2@api.com' for default user access*

```http
  GET /api/person/generateToken/{email}
```

#### Creates a new Order

***Should have as Auth, a valid JWT Token, generated previously***

```http
  POST /api/order/createOrder
```

and the body for this POST invoke, should be like this:

```
{
  "productId": "36B119C3-55FA-42FC-948D-F7F314CBDA60",
  "qty": 5
}
```
## SQL Server, Migrations & Docker

In order to have a working Database, using Docker (assuming that you already have the Docker Environment working), you should run the following command:

```
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=ab123CD*" -p 1433:1433 --name sql1 --hostname sql1n -d mcr.microsoft.com/mssql/server:2022-latest
```

After your database is online and kicking :grin:, you can populate it, either with the SQL Script in the folder Scripts (inside the project *Injection.Data*) or using migrations, with the command:

```
dotnet ef database update -p ~/YOUR_PATH/Injection/Injection.Data
```

***Important note***: This command should run on the folder 

```
~/YOUR_PATH/Injection/Injection.API
```

Also, if you run this command, tables, initial values, and also the Store Procedure, are responsible for creating new orders.


