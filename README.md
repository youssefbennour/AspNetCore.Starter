== How to run?

=== Requirements

- .NET SDK
- Docker

=== How to get .NET SDK?

To run the application, you will need to have the recent .NET SDK installed on your computer.

Click link:https://dotnet.microsoft.com/en-us/download[here] to download it from the official Microsoft website.

=== Run locally

The starter application requires Docker to run properly.

There are only 3 steps you need to start the application:

1. Make sure that you are in `/src` directory. 
2. Run `docker-compose build` to build the image of the application.
3. Run `docker-compose up` to start the application. In the meantime it will also start Postgres inside container.

The application runs on port `:8080`. Please navigate to http://localhost:8080 in your browser or http://localhost:8080/swagger/index.html to explore the API.

That's it! You should now be able to run the application using either one of the above. :thumbsup:

=== How to run Integration Tests?
To run the integration tests for the project located in the Fitnet.IntegrationTests project, you can use either the command:

`dotnet test`

or the `IDE test Explorer`. 

These tests are written using `xUnit` and require `Docker` to be running as they use `test containers` package to run PostgresSQL in a Docker container during testing. 
Therefore, make sure to have `Docker` running before executing the integration tests.
## Star History

[![Star History Chart](https://api.star-history.com/svg?repos=youssefbennour/AspNetCore.Starter&type=Date)](https://star-history.com/#youssefbennour/AspNetCore.Starter&Date)
