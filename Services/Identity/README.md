# Identity Service

Identity microservice is designed to manage access to application resources.
This microservice generates tokens for JWT-authentication.

The microservice is accessible both through the external port (3004) or through the API Gateway.

The following access roles are available:

- `Admin` - full access to any patient data.
- `User` - limited data access, only single patient data is available.

## Login

For login, use POST request:
```
http://localhost:3000/accounts/login
```

with the following body:
```
{
    "Email": "your@email.com",
    "Password": "your_password"
}
```

## Registration

For registration, use POST request:
```
http://localhost:3000/accounts/register
```

with the following body:
```
{
    "Email": "your@email.com",
    "Password": "your_password",
    "Username": "name",
    "Role": 1,
    "IsActive": true
}
```

## API Overview

For more details on API of Identity microservice, use Swagger UI:
```
http://localhost:3004/swagger
```