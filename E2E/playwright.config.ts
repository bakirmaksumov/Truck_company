import { defineConfig } from '@playwright/test';
import { baseURL, connectionString, config as e2eConfig } from './config';

export default defineConfig({
  testDir: './tests',
  // Tests share one database and drive it through the real UI (public forms + admin
  // panel), so they run serially to avoid one test's data interfering with another.
  fullyParallel: false,
  workers: 1,
  retries: 0,
  reporter: [['list'], ['html', { open: 'never' }]],
  use: {
    baseURL,
    trace: 'on-first-retry',
    screenshot: 'only-on-failure',
  },
  webServer: {
    command: 'dotnet run --no-launch-profile --project ..',
    url: `${baseURL}/health`,
    reuseExistingServer: !process.env.CI,
    timeout: 90_000,
    stdout: 'pipe',
    env: {
      ASPNETCORE_ENVIRONMENT: 'Development',
      ASPNETCORE_URLS: baseURL,
      // Overrides appsettings.Development.json so the app under test never touches
      // the real TruckCompanyDev / production database.
      ConnectionStrings__DefaultConnection: connectionString,
      Admin__Email: e2eConfig.adminEmail,
      Admin__Password: e2eConfig.adminPassword,
    },
  },
});
