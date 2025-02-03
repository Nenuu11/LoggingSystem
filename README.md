# Logging System

## Overview
Logging System is a distributed logging solution that supports multiple storage backends:
- Amazon S3-compatible storage (via HTTP client)
- Database table
- Local file system
  
This project is built using **.NET 8 Web API** for backend logging and **Angular** for frontend visualization.

---

## Technology Stack
- **Backend:** .NET 8, ASP.NET Web API, SQLServer
- **Frontend:** Angular, TypeScript, Bootstrap
- **Storage Options:**
  - Amazon S3-compatible storage (via HTTP requests only, no AWS SDK)
  - Database (SQLServer)
  - Local file system
- **Tools:** Git, Docker (to simulate aws s3 using localStack), Swagger

## Setup Instructions

### Prerequisites
- Install **.NET 8 SDK**
- Install **Node.js (v16 or later) & Angular CLI**

1) Clone the Repository
```sh
git clone https://github.com/Nenuu11/LoggingSystem.git
cd LoggingSystem
```

2) Setting up the Backend
```sh
cd DistributedLoggingSystem  (backend)
# Restore dependencies
dotnet restore
# Build the project
dotnet build
# Run the API
dotnet run
```

3) Setting up the Frontend
```sh
cd distributed-logging-system (frontend)
# Install dependencies
npm install
# Start Angular app
ng serve
```

---

## API Endpoints (Swagger)
Once the backend is running, access the **Swagger API documentation** at:
- `http://localhost:5000/swagger`


