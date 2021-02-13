**Store Balance**

- This app run inside docker and need a postgres database

**Database**
- The table will be created by migrations 
- Some data will be created by migrations

**Docker-compose**

- docker-compose -f docker-compose.yml up -d

**Use Swagger to test**

- use userId:  a12eedb1-7853-4800-b927-e48071834785

- Worker that apply future credits run every 20 seconds

- access swaggerUI [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

- Example - GET http://localhost:5000/api/Wallet/{userid}

Response
{
  "walletId": "a7609d17-9337-423f-b20d-170795fa629c",
  "balance": 1000
}
