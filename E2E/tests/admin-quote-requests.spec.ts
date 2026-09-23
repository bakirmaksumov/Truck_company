import { test, expect } from '@playwright/test';
import { loginAsAdmin, deleteQuoteRequestByName } from '../utils/admin';

test.describe('Admin - Quote Requests management', () => {
  test('admin can find a submitted request and change its status', async ({ page }) => {
    const uniqueName = `E2E Admin Quote ${Date.now()}`;

    // Create the record through the real public flow rather than touching the database.
    await page.goto('/');
    const form = page.locator('form.quote-form');
    await form.locator('#QuoteForm_FullName').fill(uniqueName);
    await form.locator('#QuoteForm_CompanyName').fill('E2E Logistics');
    await form.locator('#QuoteForm_Email').fill('e2e-quote@example.com');
    await form.locator('#QuoteForm_Phone').fill('386-843-8081');
    await form.locator('#QuoteForm_PickupLocation').fill('Daytona, FL');
    await form.locator('#QuoteForm_DeliveryLocation').fill('Atlanta, GA');
    await form.locator('#QuoteForm_FreightType').fill('Dry van');
    await form.locator('#QuoteForm_ApproximateWeight').fill('1000 lbs');
    await form.locator('#QuoteForm_PickupDate').fill('2030-01-15');
    await form.locator('#QuoteForm_Message').fill('E2E quote test message');
    await Promise.all([
      page.waitForResponse((response) => response.url().endsWith('/Home/SubmitQuote') && response.request().method() === 'POST'),
      form.getByRole('button', { name: 'Submit Request' }).click(),
    ]);
    await page.waitForLoadState('domcontentloaded');

    await loginAsAdmin(page, '/Admin/QuoteRequests');
    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);

    const row = page.locator('table.table tbody tr', { hasText: uniqueName });
    await expect(row).toBeVisible();
    await expect(row.locator('select[name="status"]')).toHaveValue('New');

    await Promise.all([
      page.waitForResponse((response) => response.url().endsWith('/Admin/QuoteRequests/UpdateStatus') && response.request().method() === 'POST'),
      row.locator('select[name="status"]').selectOption('Contacted'),
    ]);
    await page.waitForLoadState('domcontentloaded');

    // Status change auto-submits and reloads the (unfiltered) list - re-apply the search.
    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);
    const rowAfterReload = page.locator('table.table tbody tr', { hasText: uniqueName });
    await expect(rowAfterReload.locator('select[name="status"]')).toHaveValue('Contacted');

    await deleteQuoteRequestByName(page, uniqueName);
    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.getByText('No quote requests found.')).toBeVisible();
  });

  test('search filters the list down to matching requests only', async ({ page }) => {
    const uniqueName = `E2E Search Quote ${Date.now()}`;

    await page.goto('/');
    const form = page.locator('form.quote-form');
    await form.locator('#QuoteForm_FullName').fill(uniqueName);
    await form.locator('#QuoteForm_CompanyName').fill('E2E Logistics');
    await form.locator('#QuoteForm_Email').fill('e2e-quote@example.com');
    await form.locator('#QuoteForm_Phone').fill('386-843-8081');
    await form.locator('#QuoteForm_PickupLocation').fill('Daytona, FL');
    await form.locator('#QuoteForm_DeliveryLocation').fill('Atlanta, GA');
    await form.locator('#QuoteForm_FreightType').fill('Dry van');
    await form.locator('#QuoteForm_ApproximateWeight').fill('1000 lbs');
    await form.locator('#QuoteForm_PickupDate').fill('2030-01-15');
    await form.locator('#QuoteForm_Message').fill('E2E quote search test message');
    await Promise.all([
      page.waitForResponse((response) => response.url().endsWith('/Home/SubmitQuote') && response.request().method() === 'POST'),
      form.getByRole('button', { name: 'Submit Request' }).click(),
    ]);
    await page.waitForLoadState('domcontentloaded');

    await loginAsAdmin(page, '/Admin/QuoteRequests');

    await page.goto('/Admin/QuoteRequests?search=zzz-no-such-request-zzz');
    await expect(page.getByText('No quote requests found.')).toBeVisible();

    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.locator('table.table tbody tr', { hasText: uniqueName })).toBeVisible();

    await deleteQuoteRequestByName(page, uniqueName);
  });

  test('dashboard summary reflects quote request counters', async ({ page }) => {
    await loginAsAdmin(page, '/Admin/Dashboard');
    await expect(page.getByText('Total Quote Requests')).toBeVisible();
    await expect(page.getByText('New Quote Requests')).toBeVisible();
  });
});
