# Onion API Example  ⚡

It is a REST API that represents the payment logic depending on communication with an external service. This was done using clean architecture (Onion), organizing the project into use cases.


## About the Project 

I will skip the explanation of the use of clean architectures and their layers. In this example, I wanted to showcase the use of an additional layer: the communication with the external service, defining the model and services within it, which will be used in the application layer.

I have been involved in many projects with this external dependency, and I believe that it is ideal to separate external models from our domain layer, as the latter may contain information that is important for our business needs.

I think the most important aspect of communication with an external service is to use the resilience pattern, with proper configuration of HTTP requests and logging with a TraceId to track issues accurately.


## Technologies & libraries implemented:
* Entity Framework Core
* Automapper
* FluentValidation
* MediatR
* Ardalis
* OpenTelemetry
* Hangfire
* [EntityGuardian](https://github.com/byerlikaya/EntityGuardian)
* Serilog.AspNetCore
* IHttpClientFactory 
* Polly

## Future planned developments.
* Create Docker file
* Create a better readme

Consider giving a star ⭐, forking the repository, and staying tuned for updates!
