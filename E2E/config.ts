export const config = {
  port: process.env.E2E_PORT ?? '5199',
  dbServer: process.env.E2E_DB_SERVER ?? '(localdb)\\MSSQLLocalDB',
  dbName: process.env.E2E_DB_NAME ?? 'TruckCompanyE2E',
  adminEmail: process.env.E2E_ADMIN_EMAIL ?? 'e2e-admin@test.local',
  adminPassword: process.env.E2E_ADMIN_PASSWORD ?? 'E2eTestPass123!',
};

// Refuse to run against the real dev database or any name that looks like the
// production connection (Data/appsettings.Production.json uses "db65150").
const forbiddenDbNames = ['truckcompanydev', 'db65150'];
if (forbiddenDbNames.includes(config.dbName.toLowerCase())) {
  throw new Error(
    `Refusing to run E2E tests against database "${config.dbName}". ` +
      'Set E2E_DB_NAME to a dedicated test database (default: TruckCompanyE2E).',
  );
}

export const baseURL = `http://localhost:${config.port}`;

export const connectionString =
  `Server=${config.dbServer};Database=${config.dbName};Trusted_Connection=True;` +
  'MultipleActiveResultSets=true;TrustServerCertificate=True';
