# NVD Reader
[![Build Status](https://github.com/Benjamin-Stefan/NvdReader/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Benjamin-Stefan/NvdReader/actions/workflows/dotnet.yml)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

**Status:** Education

Demonstration project for [NVD Data](https://nvd.nist.gov/developers/) and C#/.NET technologies.

## Overview
The NVD Reader is a project exploring various .NET technologies, including design patterns and integration with NVD Data from NIST.

### Key Features
- **Docker / Container:** The project utilizes Docker for containerization.

- **Redis as Output Cache:** Redis is employed as an output cache for efficient data retrieval.

- **Background Service:** Incorporates a background service for handling asynchronous tasks.

- **Minimal API with OpenAPI:** The project features a minimal API with OpenAPI documentation.

- **Logging with Serilog:** Logging is implemented using the Serilog library for comprehensive log management.

- **Feature Flags:** Utilizes feature flags to enable/disable specific functionalities dynamically.

- **HttpClient with Retry Policy:** Implements HttpClient with a retry policy for improved reliability in network requests.

- **HTTP File Handling:** Supports handling of HTTP files for data exchange.

- **Store Data with MongoDB:** MongoDB is used as the data store for persistent storage.

- **Unit tests with NSubstitute:** Unit tests are created using NSubstitute for effective mocking.

## Project Structure
The project consists of two main components:

1. **Web Project:**
    - The web project serves as the main application, providing the minimal API with OpenAPI support and all other key features.
    - To start the web project, navigate to the `NvdReader.Api` directory and run the application.

    ```bash
    cd NvdReader.Api
    dotnet run
    ```

2. **Console Project:**
    - The console project is designed for background tasks and can be run independently.
    - To start the console project, navigate to the `NvdReader.Console` directory and run the application.

    ```bash
    cd NvdReader.Console
    dotnet run
    ```

## Usage
1. Open the `NvdReader.sln` solution file.
2. Set the start project to either `NvdReader.Api` or `NvdReader.Console`.
3. Optionally, modify the configuration in `appsettings.json` or `launchSettings.json`.
4. Start the project.

## Contributions
Contributions are welcome! If you'd like to contribute, follow these steps:
1. Fork the project.
2. Create a new branch (git checkout -b feature/new-feature).
3. Commit your changes (git commit -m 'Add new feature').
4. Push to your branch (git push origin feature/new-feature).
5. Open a Pull Request.

## License
This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

