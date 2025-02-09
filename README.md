# Travel in Estonia

## Overview

This project is a full-stack web application built with .NET 9 on the backend and Vite + Vue on the frontend. The
application is designed to query and display the latest prices from a data source, leveraging Hangfire for background
processing in the backend. Hangfire periodically fetches and updates the latest prices, ensuring that the frontend
always displays fresh data.
The frontend, built with Vue and powered by Vite, offers a fast and responsive user interface, communicating with the
backend through RESTful APIs.


## Technologies

- **Frontend**: Vue.js, Vite
- **Backend**: .NET 9
- **Database**: PostgreSQL
- **Other**: Docker

## Installation

### Prerequisites

Make sure you have the following installed:

- **Node.js**: [Download here](https://nodejs.org/)
- **Docker**: [Download here](https://www.docker.com/)
- **.NET SDK**: [Download here](https://dotnet.microsoft.com/download)

### Steps for development

1. **Clone the repository**:

   ```
   git clone https://github.com/karlgkallaste/travel-in-estonia.git
   ```
2. **Navigate to folder**:

   ```
   cd travel-in-estonia
   ```

3. Start the PostgreSQL Docker container
   ```
   docker-compose -f docker-compose-dev.yml up -d
   ```
4. Start the Application
