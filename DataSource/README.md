# Data Source Microservive

A data source is a microservice for simulating telemetry data collection with a specific time interval and sending data to a specified API (Sensor.API).

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


## Author

[Yury Aslamov](https://aslamovyura.github.io/)