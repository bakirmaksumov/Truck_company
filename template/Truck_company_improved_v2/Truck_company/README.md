# Express Liner website

ASP.NET Core MVC (.NET 8), EF Core, SQLite and Identity powered one-page transportation website with an Admin CMS.

## Run

```bash
dotnet restore
dotnet ef database update
dotnet run
```

The SQLite database is created as `app.db`. Admin credentials are read from `Admin:Email` and `Admin:Password`. For local development use user secrets:

```bash
dotnet user-secrets set "Admin:Email" "your@email.com"
dotnet user-secrets set "Admin:Password" "a-strong-password"
```

Open `/Admin` to sign in. Content, images, services, features, statistics, quotes, messages, contact details and SEO settings are managed there. Uploaded JPG, PNG and WEBP files (maximum 5 MB) are stored in `wwwroot/uploads`.
