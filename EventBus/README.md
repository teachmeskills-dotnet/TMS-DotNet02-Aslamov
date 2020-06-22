# Event Bus (MassTransit with RabbitMQ)

## Contracts

### Sensor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IRecordRegistered | IProcessData        | IDataProcessed        |+
| IRecordDeleted    |                     |                       |-
| IRecordUpdated    |                     |                       |-
| ISensorRegistered |                     |                       |-
| ISensorDeleted    |                     |                       |-
| ISensorUpdated    |                     |                       |-

### DataProcessor.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IDataProcessed    | IRegisterReport     | IProcessData          |+

### Report.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IReportRegistered |                     | IRegisterReport       |-
|                   |                     | IRecordDeleted        |-
|                   |                     | ISensorDeleted        |-
|                   |                     | IProfileDeleted       |-

### Profile.API

| Publish Events    | Send Commands       | Subcribed             |
|-------------------|---------------------|-----------------------|
| IProfileRegistered|                     |                       |-
|                   |                     |                       |-

## Author

[Yury Aslamov](https://aslamovyura.github.io/)