# Data Source Microservive

A data source is a microservice for modeling the colletion and transmission of medical telemetry (temperature/acoustic).

Data collection time interval can be configured through the microservice API.
The collected data is transferred to the application API Gateway (and routing to Sensor.API for storage).

## Get Started

To start data generation, send the following POST request:

```
http://localhost:3010/api/datasource/start
```

To stop DataSource microservice, send the following POST request:

```
http://localhost:3010/api/datasource/stop
```

To configure data generation parameters (sensor serial, data type, generation time interval), use the following POST request:

```
http://localhost:3010/api/datasource/configure
```

To check microservice state (working/stopped), use the following GET request:

```
http://localhost:3010/api/datasource/hc
```

## Additional Services

Use Swagger UI service for more information on the DataSource API:

```
http://localhost:3010/swagger
```

Use Jaeger tracing service to verify that the generated data is sent to microservices through Gateway.API:

```
http://localhost:16686
```