# E2E tests — Truck_company (Express Liner LLC website)

Playwright (TypeScript) end-to-end tests for the public site and the `/Admin` panel.

## Database used

Tests run the app against a **dedicated LocalDB database**, `TruckCompanyE2E` by default
(never the real `TruckCompanyDev` dev database, and never the production database from
`appsettings.Production.json`). The connection string is injected as an environment
variable when Playwright starts the app — it does **not** read `appsettings.Development.json`.

[config.ts](config.ts) refuses to start if `E2E_DB_NAME` is set to `TruckCompanyDev` or
`db65150` (the production database name), as a guard against misconfiguration.

The app creates/migrates this database automatically on startup (`SeedData.InitializeAsync`
calls `dbContext.Database.MigrateAsync()`), so no manual DB setup is required — LocalDB will
create `TruckCompanyE2E` the first time the tests run.

An admin user for the test database is also seeded automatically from the `Admin__Email` /
`Admin__Password` environment variables passed to the app (see `SeedData.cs`).

## Running

```powershell
cd E2E
npm install
npx playwright install --with-deps chromium
npm test
```

Playwright's `webServer` config starts `dotnet run --project ..` for you (pointed at
`TruckCompanyE2E`) and waits for `/health` to respond, then tears it down after the run.

To use a different test database/admin account, copy `.env.example` to `.env`, edit it, and
load it into your shell before running `npm test` (or set the variables directly).

## What is covered

| File | Scenario |
|---|---|
| `tests/public-quote-form.spec.ts` | Public "Request a quote" form: valid submission creates a `New` request; missing required field blocks submission, no record created |
| `tests/public-contact-form.spec.ts` | Public "Contact us" form: valid submission creates a message; missing required field blocks submission |
| `tests/admin-login.spec.ts` | Admin login: correct credentials reach `/Admin/Dashboard`; wrong password shows an error and denies access |
| `tests/admin-access-control.spec.ts` | Every `/Admin/*` route redirects anonymous visitors to the login page |
| `tests/admin-quote-requests.spec.ts` | Admin finds a request via search, changes its status, change persists after reload; search filters correctly; dashboard counters are visible |
| `tests/admin-contact-messages.spec.ts` | Admin finds a submitted message via search and deletes it |
| `tests/admin-services.spec.ts` | Services CRUD and image upload: create, edit, replace image, delete |
| `tests/admin-features-statistics.spec.ts` | Features CRUD and Statistics CRUD |
| `tests/admin-settings-upload.spec.ts` | Website settings save and hero image attachment upload |
| `tests/public-routes.spec.ts` | Health endpoint, Privacy page, sitemap XML, and unknown-route 404 |
| `tests/responsive.spec.ts` | Mobile, tablet and desktop layout: no horizontal overflow, usable forms, responsive navigation |
| `tests/visual-home.spec.ts` | Full-page visual regression baselines for mobile, tablet and desktop |

All test data is created through the real UI (public forms, admin search/delete), not by
writing to the database directly, and every test deletes the records it created so the
database returns to its prior state after a run.

## Visual tests

Run visual comparison with:

```powershell
npm.cmd run test:visual
```

When an intentional design change is made, regenerate the baselines explicitly:

```powershell
npx.cmd playwright test tests/visual-home.spec.ts --update-snapshots
```

Baseline images are stored next to `visual-home.spec.ts` in
`tests/visual-home.spec.ts-snapshots/`.

## Known limitations / not covered yet

- No cross-browser run configured; add a `test:browsers` script if needed later.

## Rules followed while writing these tests

- Never touches the production database or `appsettings.Production.json`.
- No EF Core migrations or schema changes were made or are needed.
- Application code (`Controllers/`, `Views/`, `Data/`) was not modified for these tests.
