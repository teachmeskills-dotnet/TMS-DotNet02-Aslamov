# Event Bus (MassTransit with RabbitMQ)

The microservices in the application are completely independent and are not aware of the existence of other microservices.

Microservices use the event bus for communication. The event bus is implemented on the [MassTransit](https://masstransit-project.com/) and uses the [RabbitMQ](https://www.rabbitmq.com/) as a message broker.

## Commands & Events

Each microservice can be subscribed to certain events and commands, and can also send them.

A **command** can only be received by one microservice. At the same time, an unlimited number of microservices can be subscribed to **events**.

The commands and events used by each microservice are presented below.

### Sensor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IRecordRegistered | IProcessData        | IDataProcessed        |
| IRecordDeleted    |                     |                       |
| IRecordUpdated    |                     |                       |
| ISensorRegistered |                     |                       |
| ISensorDeleted    |                     |                       |
| ISensorUpdated    |                     |                       |

### DataProcessor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IDataProcessed    | IRegisterReport     | IProcessData          |

### Report.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IReportRegistered |                     | IRegisterReport       |
|                   |                     | IRecordDeleted        |
|                   |                     | ISensorDeleted        |
|                   |                     | IProfileDeleted       |

### Profile.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IProfileRegistered|                     |                       |
|                   |                     |                       |

## Author

[Yury Aslamov](https://aslamovyura.github.io/)