import { test, expect } from '@playwright/test';

// Every controller under Areas/Admin is [Authorize(Roles = "Admin")]; anonymous
// visitors must always be bounced to the login page, never see the page itself.
const protectedPaths = [
  '/Admin/Dashboard',
  '/Admin/QuoteRequests',
  '/Admin/ContactMessages',
  '/Admin/Services',
  '/Admin/Features',
  '/Admin/Settings',
  '/Admin/Statistics',
];

test.describe('Admin area access control', () => {
  for (const path of protectedPaths) {
    test(`anonymous access to ${path} redirects to login`, async ({ page }) => {
      const response = await page.goto(path);
      await expect(page).toHaveURL(/\/Identity\/Account\/Login/);
      expect(response?.status()).toBeLessThan(400);
    });
  }
});
