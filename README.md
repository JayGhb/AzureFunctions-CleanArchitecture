# Azure Functions - Clean Architecture
![CI/CD](https://github.com/JayGhb/AzureFunctions-CleanArchitecture/actions/workflows/main_azurefunctionscleanarchitecture.yml/badge.svg)

This is a reference project to complement the "An application with respect to itself" [series of posts](https://medium.com/@manoloudisiason/an-application-with-respect-to-itself-introduction-6e268c6bbe7a). The project's aim is to showcase the use of five, in my opinion, core aspects that each production system of the appropriate size & business criticality oughts to incorporate and yet they are so often overlooked, resulting production issues, inefficiency when solving such, lost data and much more.

## Focus Points
1. Input Validation
2. Logging and request correlation
3. Exception Handling
4. (A)synchronous retry mechanisms
5. Asynchronous methods and graceful shutdown

## Prerequisite
The project adapts elements of the Clean Architecture approach into Azure Functions (In-process execution mode). Following that, this project and the series assume the reader is already familiar with:
- The Clean Architecture approach
- Dependency Injection
- The CQRS Pattern
- The Mediator pattern and the use of MediatR

## Work in progress
The project is a work in progress and will be enriched as more posts are added to the series.
