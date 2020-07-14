# Event Bus (MassTransit with RabbitMQ)

Application microservices are completely independent and are not aware of the existence of other microservices.

Microservices use the event bus for communication. The event bus is implemented on the [MassTransit](https://masstransit-project.com/) and uses [RabbitMQ](https://www.rabbitmq.com/) as a message broker.

## Commands & Events

Each microservice can be subscribed to certain events and commands, and can also send them.

A **command** can be received by only one microservice. At the same time, an unlimited number of microservices can be subscribed to **events**.

The commands and events used by each microservice are presented below.

### Sensor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| RecordRegistered  | ProcessData         | DataProcessed         |
| RecordDeleted     |                     | UserDeleted           |
| SensorDeleted     |                     |                       |

### DataProcessor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| DataProcessed     | RegisterReport      | ProcessData           |

### Report.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
|                   |                     | RecordDeleted         |
|                   |                     | RegisterReport        |

### Identity.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| UserDeleted       |                     | AccountDeleted        |

### Profile.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| AccountDeleted    |                     | UserDeleted           |
