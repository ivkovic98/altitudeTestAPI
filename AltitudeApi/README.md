# AltitudeApi

ASP.NET Core 8.0 Web API for product management with user authentication, image upload, and analytics.

## Features
- JWT Authentication
- Product CRUD operations
- User management with profile photos
- Image upload via Cloudinary
- Statistics dashboard
- Clean Architecture (Business/Data/API layers)

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server LocalDB

## Quick Start

1. **Clone and navigate to project**
   ```bash
   git clone [repository-url]
   cd AltitudeApi
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Run database migrations**
   ```bash
   cd AltitudeApi
   dotnet ef database update
   ```

4. **Start the application**
   ```bash
   dotnet run
   ```

5. **Access the API**
   - API: `https://localhost:7057`
   - Swagger UI: `https://localhost:7057/swagger`

## Alternative: Docker Setup

```bash
docker-compose up -d
```
- API will be available at `http://localhost:5000`
- Includes SQL Server database automatically

## API Endpoints

- **Auth**: `POST /api/auth/login`
- **Products**: `GET|POST|PUT|DELETE /api/product`
- **Users**: `GET|PUT /api/user/profile`, `POST /api/user/upload-photo`
- **Statistics**: `GET /api/statistics`

## Testing

The application includes a data seeder that creates sample data on startup. Use Swagger UI to explore and test all endpoints.

## Configuration Notes

- Database: Uses LocalDB by default (no additional setup needed)
- JWT: Pre-configured with development settings
- Image Upload: Cloudinary integration (optional - will work without it)
- CORS: Configured for `localhost:5173` (React/Vite frontend) 