# TMS-DotNet02-Aslamov

![.NET Core](https://github.com/teachmeskills-dotnet/TMS-DotNet02-Aslamov/workflows/.NET%20Core/badge.svg)

The main idea of the application is to develop medical web-applicacation based on microservices for patients health care.
Application contains web service for user management and the set of services to simulate the sending of medical telemetry data to server.
Services for data storage & processing are simple CRUD microservices.

## Getting Started

## Application settings

## Deployment of Docker container on Heroku

## Microservices

The microservices in the application are completely independent and are not aware of the existence of other microservices.

- [x] [API Gateway]()
- [x] [Sensor Microservice]()
- [x] [DataProcessor Microservice]()
- [x] [Identity Microservice]()
- [x] [Profile Microservice]()
- [x] [Report Microservice]()
- [x] [Event Bus]()
- [ ] [Data Source]()
- [ ] [Web]()

## Built with

- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/)
- [Microservices](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/)
- [REST API](https://restfulapi.net/)
- [Docker](https://www.docker.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Automapper](https://automapper.org/)
- [Health check](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1)
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
- [Serilog](https://serilog.net/)
- [MassTransit](https://masstransit-project.com/)
- [RabbitMQ](https://www.rabbitmq.com/)

## Author

[Yury Aslamov](https://aslamovyura.github.io/)

## License

This project is under the MIT License - see the [LICENSE.md](https://github.com/teachmeskills-dotnet/TMS-DotNet02-Aslamov/blob/master/LICENSE) file for details.
