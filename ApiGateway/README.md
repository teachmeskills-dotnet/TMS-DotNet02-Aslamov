# API Gateway

API Gateway is a single entry point for a microservice application. This service routes to the designated microservice.

In addition, each microservice has an external port for the direct access.

| Microservice         | Container name      | Port    |
|----------------------|---------------------|---------|
| Gateway.API          | gateway.api         | 3000:80 |
| Sensor.API           | sensor.api          | 3001:80 |
| Report.API           | report.api          | 3002:80 |
| Profile.API          | profile.api         | 3003:80 |
| Identity.API         | identity.api        | 3004:80 |
| DataProcessor.API    | dataprocessor_1.api | 3005:80 |
| DataProcessor.API    | dataprocessor_2.api | 3006:80 |

Microservices Health Check (through Gateway API)

| Microservice         | Health check URL                      |
|----------------------|---------------------------------------|
| Gateway.API          | localhost:3000/hc                     |
| Sensor.API           | localhost:3000/sensors.api/hc         |
| Report.API           | localhost:3000/report.api/hc          |
| Profile.API          | localhost:3000/profile.api/hc         |
| Identity.API         | localhost:3000/identity.api/hc        |
| DataProcessor.API    | localhost:3000/dataprocessor_1.api/hc |
| DataProcessor.API    | localhost:3000/dataprocessor_2.api/hc |
