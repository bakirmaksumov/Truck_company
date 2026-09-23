# Truck_company Deployment (MonsterASP.NET)

This project is an ASP.NET Core MVC (.NET 8) application with EF Core, SQL Server, ASP.NET Core Identity, Razor Views, and an Admin CMS under `/Admin`.

## 1) Required production environment variables

Set these in MonsterASP.NET Website Environment Variables:

- `ConnectionStrings__DefaultConnection`
- `Admin__Email`
- `Admin__Password`
- `Uploads__PhysicalPath`
- `Uploads__RequestPath`
- `Smtp__Host` (Hostinger: `smtp.hostinger.com`)
- `Smtp__Port` (`587` with STARTTLS, or `465` with SSL)
- `Smtp__EnableSsl`
- `Smtp__UserName` (`info@expressliner.com`)
- `Smtp__Password`
- `Smtp__FromEmail` (`info@expressliner.com`)
- `Smtp__ToEmail` (`info@expressliner.com`)
- `GoogleRecaptcha__SiteKey`
- `GoogleRecaptcha__SecretKey`
- `ASPNETCORE_ENVIRONMENT`

Recommended:

- `ASPNETCORE_ENVIRONMENT=Production`

The public domain should be configured as `expresslinerco.com` in Hostinger and its DNS
should point to the deployed application. The form submissions are saved in the admin
database and emailed to `info@expressliner.com` when SMTP is configured.

### Connection string
Paste your MonsterASP.NET SQL Server connection string into:

- `ConnectionStrings__DefaultConnection`

Do **not** store this value in source control.

### Admin credentials
Set:

- `Admin__Email`
- `Admin__Password`

In Production, if these are missing, admin user seeding is skipped intentionally and a critical warning is logged.

### Upload storage
Defaults (development):

- `Uploads__PhysicalPath=wwwroot/uploads`
- `Uploads__RequestPath=/uploads`

For MonsterASP.NET, set `Uploads__PhysicalPath` to a writable server folder.

---

## 2) Local development setup

Use User Secrets for local-only values:

1. Initialize user secrets (if needed):
   - `dotnet user-secrets init`
2. Set values:
   - `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\MSSQLLocalDB;Database=TruckCompanyDev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"`
   - `dotnet user-secrets set "Admin:Email" "your-admin-email"`
   - `dotnet user-secrets set "Admin:Password" "your-strong-password"`

---

## 3) Apply migrations

For target database:

1. Set `ConnectionStrings__DefaultConnection` to target SQL Server.
2. Run:
   - `dotnet ef database update`

Migration in this project:

- `InitialSqlServer`

Legacy SQLite migrations were archived to `Data/Migrations.Sqlite.Backup` and are not compiled.

If remote SQL access is available only from the hosted Website, migration runs automatically on first hosted startup via `Database.MigrateAsync()`.

---

## 4) Visual Studio publish steps (Web Deploy)

1. Activate WebDeploy in MonsterASP.NET panel.
2. Download `.publishSettings` from MonsterASP.NET.
3. Open the project in Visual Studio.
4. Right-click project -> **Publish**.
5. Choose **Import Profile** and import the downloaded `.publishSettings`.
6. Open **Settings** and use:
   - Configuration: `Release`
   - Target Framework: `net8.0`
   - Deployment Mode: `Framework-dependent`
   - Target Runtime: `Portable`
   - Remove additional files at destination: **Disabled** (unless confirmed safe)
7. Publish.
8. In MonsterASP.NET panel, configure environment variables listed above.
9. Restart the site/app pool.

Do not commit `.publishSettings`, `.pubxml.user`, or any deployment credentials.

Do not publish secrets, local SQLite files, or debug artifacts.

---

## 5) Admin access

- Admin login URL: `/Identity/Account/Login`
- Admin area: `/Admin`
- `/Admin` is protected with role-based authorization (`Admin`).

---

## 6) Health check

- Endpoint: `/health`
- Expected response: HTTP 200 with JSON status.

---

## 7) Logs and troubleshooting

### Application logs
- Use MonsterASP.NET logging viewer / IIS logs.
- Startup warnings include missing production admin credentials.

### Temporarily enable stdout logging (IIS)
In `web.config`, set:

- `stdoutLogEnabled="true"`

After troubleshooting, set it back to `false`.

---

## 8) Backups

Back up regularly:

1. SQL Server database (full backup + transaction log strategy as applicable).
2. Uploaded files directory (`Uploads__PhysicalPath`).

Restore both together for complete recovery.
