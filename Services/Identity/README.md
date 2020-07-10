# Identity Service

Identity microservice is designed to control access to application resources.
This microservie generates tokens for JWT authentication.

The microservice is accessible both through the external port (3004) and through the API gateway.

The following access roles are available:

- Admin - full access to the whole patient data.
- User - limited acces (only for a single patient data).