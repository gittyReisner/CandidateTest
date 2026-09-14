# Requests Management System

A full-stack application for managing and filtering Requests.

The solution includes a .NET 8 ASP.NET Core Web API backend and a React frontend.

## Running the Backend

1. Open the solution in Visual Studio.
2. Set the API project as the startup project.
3. Run the application.
4. The API will be available at the configured HTTPS URL.

Swagger can be used to test the API endpoints.

## Running the Frontend

1. Open a terminal in the frontend project folder.
2. Install the dependencies:

```bash
npm install
```

##Start the React development server:

```bash
npm run dev
```

##Open the URL displayed by React in the browser

## Technology Choices

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core

.NET 8 and ASP.NET Core were chosen because they provide a strong and maintainable platform for building REST APIs.

Entity Framework Core is used for data access and allows the filtering, sorting and pagination logic to be translated into database queries.

### Frontend
- React

React was chosen for the frontend because it provides a simple component-based approach for building the filter form, requests table and pagination.

## Assumptions

- The current user is identified by the `X-User-Id` request header.
- Administrator access is identified by the `X-Is-Admin` request header.
- A regular user can see Requests that they own or are assigned to.
- An Administrator can see all Requests.
- Pagination is limited to a maximum page size of 100.
- The current implementation uses an EF Core InMemory database for the assessment environment.
- In a production environment with millions of records, a relational database such as Azure SQL would be used.

## Technical Decisions

### Server-side Filtering, Sorting and Pagination

Filtering, sorting and pagination are implemented on the server side rather than in the frontend.

An alternative would be to load all Requests into the frontend and perform these operations in React.

The server-side approach was chosen because the system may contain millions of records. This reduces the amount of data transferred to the client and allows the database to perform the filtering, sorting and pagination efficiently.

### Asynchronous Communication

For communication between the Requests Service and Notifications Service, the architecture uses asynchronous messaging through a message broker.

An alternative would be direct synchronous communication between the services.

The asynchronous approach was chosen because the Notifications Service may be temporarily unavailable. Using a message broker allows events to remain available for processing and supports retries without blocking the Requests Service.

## Tests

The backend includes unit tests covering the main filtering, sorting and authorization scenarios.

The tests cover:
- Administrator access to all Requests
- Regular user access to owned or assigned Requests
- Partial Request Number filtering
- Multiple Status filtering
- Creation date range filtering
- Request Type filtering
- Sorting by creation date

To run the tests:

```bash
dotnet test
```

##Not Completed / Future Improvements

The current solution focuses on the requirements of the assessment.

Possible future improvements include:

Returning pagination metadata such as total count and hasNextPage.
Adding additional validation and test coverage.
Clean and smart management of client-side components and sub-components.
