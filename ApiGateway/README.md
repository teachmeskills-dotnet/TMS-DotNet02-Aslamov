# API Gateway

API Gateway is a single entry point for a microservice application. This service routes to the designated microservice. 

In addition, each microservice has an external port and can be accessed from outside.

| Microservice         | Container name      | Port    |
|----------------------|---------------------|---------|
| SQL Server           | sqldata             | 1433:80 |
| Gateway.API          | gateway.api         | 3000:80 |
| Sensor.API           | sensor.api          | 3001:80 |
| DataProcessor.API    | dataprocessor.api   | 3002:80 |
| Profile.API          | profile.api         | 3003:80 |
| Identity.API         | identity.api        | 3004:80 |
| Report.API           | report.api          | 3005:80 |

## Author

[Yury Aslamov](https://aslamovyura.github.io/)