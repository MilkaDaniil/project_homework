# Saucelab App test with Playwright (Homework)

## Prerequisites

1. **.NET SDK 8.0 or higher**
    - Download and install the .NET SDK from [Microsoft’s official website](https://dotnet.microsoft.com/).

2. **Docker (optional)**
    - Ensure Docker is installed and running on your machine. You can download Docker Desktop from [Docker’s official website](https://www.docker.com/).

3. **GitHub Actions** (optional for CI/CD pipeline)
    - Ensure you have a GitHub account and appropriate permissions to run GitHub Actions workflows.

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/MilkaDaniil/project_homework
cd project_homework
```

### 2. Install Dependencies
Ensure the required packages for Playwright and .NET are installed:
```bash
dotnet restore
```

### 3. Run Tests Locally
To execute Playwright tests locally, run the following command:
```bash
dotnet test
```

## Running with Docker

This project supports running Playwright tests in a Docker container. The repository includes a Dockerfile to create an image and run tests in a containerized environment.

### 1. Build Docker Image
```bash
docker build -t docker-image-name .
```

### 2. Run Docker Container
```bash
docker run --rm docker-image-name
```

The `--rm` flag ensures that the container is removed after execution.

## CI/CD Pipeline: GitHub Actions

This repository includes a pre-configured GitHub Actions pipeline to run Playwright tests. The pipeline is located in the `.github/workflows` directory.

### Credentials
The GitHub Actions pipeline requires the following credentials:
- **Username:** `admin`
- **Password:** `admin`

These credentials are used as defaults and can be configured in the workflow file if needed.

To trigger the pipeline, push a commit or manually trigger it from the GitHub Actions tab in your repository.

## Additional Information

- For more details on Playwright and its capabilities, visit the [official Playwright documentation](https://playwright.dev/).
- For .NET-specific features, refer to the [.NET documentation](https://learn.microsoft.com/en-us/dotnet/).
- For Docker support and best practices, consult the [Docker documentation](https://docs.docker.com/).

