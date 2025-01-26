= .NET Modular Monolith Starter
:toc: macro

A production-ready starter template for building modular monolith applications using .NET 9.0, implementing the Backend-For-Frontend (BFF) pattern with Keycloak authentication. 

If you're not familiar with the modular monolith concept, you can find an explanation for this choice in the https://github.com/evolutionary-architecture/evolutionary-architecture-by-example/blob/main/README.adoc#-modular-monolith[Evolutionary Architecture's documentation].

== Features

=== 🏗️ Modular Monolith Architecture
* Clean separation of concerns
* Module-based structure
* Easy to extend and maintain

=== 🔐 Authentication & Authorization
* BFF (Backend-For-Frontend) pattern implementation
* Keycloak integration

=== 🚌 Event-Driven Architecture
* Transactional Outbox Pattern implementation
* Message-driven communication between modules

=== 📊 Observability
* OpenTelemetry integration
* Grafana dashboards
* Jaeger for tracing
* Prometheus metrics

=== 🧪 Testing
* Unit testing setup with xUnit
* Integration testing with TestContainers

== Prerequisites

* .NET SDK 9.0
* Docker

== Getting Started

=== 1. Clone the repository

=== 2. Navigate to the source directory
[source,bash]
----
cd src
----

=== 3. Build the Docker images
[source,bash]
----
docker compose build
----

=== 4. Start the application
[source,bash]
----
docker compose up
----

[IMPORTANT]
====
For local development with Docker Compose, additional setup is required to handle Keycloak authentication properly. Please refer to the link:LOCAL_DEVELOPMENT.adoc[Local Development Guide] for detailed instructions.
====

[NOTE]
====
Integrating the BFF with a front-end requires special attention,  I recommend reading this detailed article:
https://wrapt.dev/blog/standalone-duende-bff-for-any-spa
====

The application will be available at:

* BFF: http://localhost:8081
* API: http://localhost:8080
* Swagger UI: http://localhost:8080/swagger/index.html

== Project Structure

[source]
----
src/
├── Starter/              # Main API application
├── Starter.BFF/          # Backend-For-Frontend application
├── Starter.Common/       # Shared libraries and utilities
├── Starter.UnitTests/    # Unit tests
└── Starter.IntegrationTests/ # Integration tests
----

== Authentication Flow

The project uses Keycloak as the identity provider with the BFF pattern:

. Frontend applications communicate with the BFF
. BFF Authenticates user by cookie
. If user is not authenticated, BFF challenges the user to Keycloak & deals a cookie[secure, http only and SameSite strict] to the front-end
. BFF proxies authenticated requests to the main API
. API validates JWT tokens and handles authorization

=== Direct API Access

If you need to bypass the BFF and access the APIs directly, you can obtain an access token from Keycloak using the following command:

[source,bash]
----
curl --location 'http://localhost:7002/realms/starter-realm/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=password' \
--data-urlencode 'username={your username}' \
--data-urlencode 'password={your password}' \
--data-urlencode 'client_id=starter-client' \
--data-urlencode 'client_secret=2UxRNf8TY7jwycJCWcEWTWz3joSqQlLO'
----

The response will contain an access token that can be used to authenticate direct API calls by including it in the `Authorization` header as a Bearer token.

== Running Tests

=== Unit Tests
[source,bash]
----
dotnet test src/Starter.UnitTests
----

=== Integration Tests
[source,bash]
----
dotnet test src/Starter.IntegrationTests
----

[NOTE]
====
Integration tests require Docker to be running as they use TestContainers.
====

== Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

== Acknowledgments

This project is based on the excellent work from the https://github.com/evolutionary-architecture/evolutionary-architecture-by-example[Evolutionary Architecture by Example] repository. Special thanks to all the creators and contributors of the original project for providing such a great foundation for building modern .NET applications.

Original repository maintainers and contributors can be found at: https://github.com/evolutionary-architecture/evolutionary-architecture-by-example/graphs/contributors

== License

This project is licensed under the MIT License - see the link:LICENSE[LICENSE] file for details.
