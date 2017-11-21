# FraudTransactions
## Synthetic Financial Manager System For Fraud Transactions ASP MVC
### Author: Juan Camilo Zapata
### Phone : +57 3164380458

---

To deploy the entire solution do the following steps:

1. Deploy the database scripts, for this, you need minimum an instance of SQL server express
execute the script "1.Scripts\TransactionsDB_script.sql" this will create the database called TransactionsDB, note that the database will be empty aside from the roles table.

2. Install SQL Server Data tools to be able to run the ETL, when ready, you can open the ETL solution and configure the two connection strings one for the database (target) and the other for the source file (kaggle source file), when you execute the ETL the kaggle CSV file will be loaded into the database specifically the transactions table(6M records more or less) and an index routine has been executed to speed up searches.
Alternatively, you can configure the XML file called config.dtsConfig, and run a CMD with the following command 
first go to the route where the package.dtsx is located and open a cmd window there to execute the command
DTEXEC /f "Package.dtsx" /CONFIGFILE "config.dtsConfig""  /REPORTING V

3. Web Application and Web API
open the solution directly on Visual Studio and be sure to update the web config values on the following file
"\FraudTransactions\Web.config"

4. When you run the entire solution, the ASP MVC site will be up and the WebAPI will be up as well
5. First, you should start by creating a couple of users with their respective role
6. Anytime you can use the WebAPI to consume transactions operations via REST, this is an example of a request that you can run on POSTMAN, all the requests need to have authentication tokens:

```
GET /API/TransactionsApi/GetTransactionSearch?term=NAMEDEST&amp;op=AND&amp;isFraud=false&amp;nameDest=M1979787155 HTTP/1.1
Host: localhost:17618
Authorization: Basic YWRtaW5pc3RyYXRvcjpMb2NrZTMzNA==
Cache-Control: no-cache
Postman-Token: e8360a0d-8b1f-c3f5-f793-1c4f9fa9166f
```

**Note**
the: Authorization: Basic YWRtaW5pc3RyYXRvcjpMb2NrZTMzNA== line is the composition of UserName:Password encoded to a base64
