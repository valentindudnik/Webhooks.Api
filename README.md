# Webhooks Api v1.0.0

Webhooks Api contains Invoice APIs to manage different types of invoices:

- POST:
/api/v{version}/Invoices

- GET:
/api/v{version}/Invoices

- PUT:
/api/v{version}/Invoices/{invoiceId}

- DELETE:
/api/v{version}/Invoices/{invoiceId}

- GET:
/api/v{version}/Invoices/{invoiceId}

- POST:
/api/v{version}/Invoices/{invoiceId}/approve

# Hosting

Host Name: 
http://webhooks-api.westeurope.cloudapp.azure.com/

Swagger UI: 
http://webhooks-api.westeurope.cloudapp.azure.com/swagger

# Technological Stack:

- .NET6 / C#

- REST

- MS Azure

- Azure Kubernetes

- ApplicationInsights

- Docker

- RabbitMQ (https://www.cloudamqp.com/)

- MongoDB / CosmoDB

- GitHub / Git

- Swagger

- Visual Studio 2022 / Robo 3T 1.4.3 / MongoDb Compass

# Deployment

[Create an image pull secret:](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-auth-kubernetes)

```
kubectl create secret docker-registry <secret-name> \
    --namespace <namespace> \
    --docker-server=<container-registry-name>.azurecr.io \
    --docker-username=<service-principal-ID> \
    --docker-password=<service-principal-password>
```

YAML:
https://github.com/valentindudnik/webhooks.api/tree/main/yaml/webhooks.api.yml

# NuGet Packages:

[Webhooks.RabbitMQ.Client v1.0.0](https://www.nuget.org/packages/Webhooks.RabbitMQ.Client/1.0.0?_src=template)

[Webhooks.RabbitMQ.Models v1.0.0](https://www.nuget.org/packages/Webhooks.RabbitMQ.Models/1.0.0?_src=template)

GitHub Repository:
https://github.com/valentindudnik/webhooks.rabbitmq
