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
   git clone https://github.com/ivkovic98/altitudeTestAPI.git
   cd AltitudeApi
   ```

2. **Configure Cloudinary (Optional)**
   Add your Cloudinary credentials to `appsettings.json`:
   ```json
   "Cloudinary": {
     "CloudName": "your-cloud-name",
     "ApiKey": "your-api-key",
     "ApiSecret": "your-api-secret"
   }
   ```

3. **Restore packages**
   ```bash
   dotnet restore
   ```

4. **Run database migrations**
   ```bash
   cd AltitudeApi
   dotnet ef database update
   ```

5. **Start the application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - API: `https://localhost:7057`
   - Swagger UI: `https://localhost:7057/swagger`

## Alternative: Docker Setup

```bash
docker-compose up -d
```
- API will be available at `http://localhost:5000`  
- Includes SQL Server database automatically

## Default Admin Credentials

The application seeds a default admin user on startup:
- **Email**: `admin@test.com`
- **Password**: `admin123`

Use these credentials to log in and test the API endpoints.

## API Endpoints

- **Auth**: `POST /api/auth/login`
- **Products**: `GET|POST|PUT|DELETE /api/product`
- **Users**: `GET|PUT /api/user/profile`, `POST /api/user/upload-photo`
- **Statistics**: `GET /api/statistics`

## Frontend Application

This API works with the companion React frontend application:
- **Frontend Repository**: [https://github.com/ivkovic98/altitudeTest](https://github.com/ivkovic98/altitudeTest)
- **Frontend URL**: `http://localhost:5173` (when running locally)

## Configuration Notes

- **Database**: Uses SQL Server LocalDB by default (configured in `appsettings.json`)
- **JWT**: Pre-configured with development settings
- **Image Upload**: Cloudinary integration (optional - will work without configuration)
- **CORS**: Configured for `localhost:5173` (React frontend)

## Testing

The application includes a data seeder that creates sample data and admin user on startup. Use Swagger UI to explore and test all endpoints with the provided admin credentials. 