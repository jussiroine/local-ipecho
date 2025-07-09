# local-ipecho

A self-hosted IP echo service implemented in C# (.NET Core 8.0) that runs in a Docker container.

## Features

- Returns the client's IP address for both HTTP GET and POST requests
- Handles proxy headers (X-Forwarded-For, X-Real-IP) for proper IP detection
- Lightweight container suitable for internal network deployment
- Can be exposed through firewall rules

## Building and Running

### Using Docker

Build the container:
```bash
docker build -t local-ipecho .
```

Run the container:
```bash
docker run -d -p 8080:8080 --name ipecho local-ipecho
```

### Using Docker Compose

Start the service with Docker Compose using the provided `docker-compose.yml`:
```bash
docker compose up -d
```

### Local Development

Build and run locally:
```bash
dotnet build
dotnet run
```

## Usage

The service responds to both GET and POST requests at the root path:

```bash
# GET request
curl http://localhost:8080/

# POST request  
curl -X POST http://localhost:8080/
```

Both will return just the client's IP address as plain text.

## Configuration

The service runs on port 8080 by default. This can be configured via the `Urls` setting in `appsettings.json` or environment variables.

## Deployment

For internal network deployment:
1. Build and run the Docker container
2. Configure firewall rules to expose port 8080 as needed
3. The service will properly detect client IPs even when behind proxies
