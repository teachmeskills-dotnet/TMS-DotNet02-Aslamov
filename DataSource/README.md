# Data Source Microservive

A data source is a microservice for generation and transmission of medical telemetry (temperature/acoustic).
Data Source microservice is available on ports 3010 or 3011.

Data collection time interval can be configured through the microservice API.
The collected data is transferred to the application API Gateway (and routing to Sensor.API microservice for storage).

## Get Started

### 1. Configure data generation

To configure data generation parameters (sensor serial, data type, generation time interval), use the following POST request:

```
http://localhost:3010/api/datasource/configuration
```

Body of the POST request in this case can be the following:

```
{
	"SensorSerial" : "123456789",
    "DataType": "Temperature",
    "GenerationTimeIntervalSeconds": "10"
}
```

### 2. Start/stop data generation

To start data generation, send the following POST request (with empty body):

```
http://localhost:3010/api/datasource/start
```

To stop DataSource microservice, send the following POST request (with empty body):

```
http://localhost:3010/api/datasource/stop
```

To check microservice state (working/stopped), use the following GET request:

```
http://localhost:3010/api/datasource/hc
```

## Use Additional Services

Use Swagger UI service for more information on the DataSource API:

```
http://localhost:3010/swagger
```

Use Jaeger tracing service to verify that the generated data is sent to microservices through Gateway.API:

```
http://localhost:16686
```