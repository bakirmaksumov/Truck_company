import { test, expect } from '@playwright/test';
import { loginAsAdmin, deleteQuoteRequestByName } from '../utils/admin';

test.describe('Public quote request form', () => {
  test('valid submission creates a new quote request with status New', async ({ page }) => {
    const uniqueName = `E2E Quote ${Date.now()}`;

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

    // Verify through the admin panel (real app behavior), not by touching the database.
    await loginAsAdmin(page, '/Admin/QuoteRequests');
    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);
    const row = page.locator('table.table tbody tr', { hasText: uniqueName });
    await expect(row).toBeVisible();
    await expect(row.locator('select[name="status"]')).toHaveValue('New');

    await deleteQuoteRequestByName(page, uniqueName);
  });

  test('missing required field blocks submission and creates no record', async ({ page }) => {
    const uniqueName = `E2E Quote Invalid ${Date.now()}`;

    await page.goto('/');
    const form = page.locator('form.quote-form');
    // FullName is left empty on purpose - it is a required field.
    await form.locator('#QuoteForm_Email').fill('e2e-quote-invalid@example.com');
    await form.locator('#QuoteForm_Phone').fill('386-843-8081');
    await form.locator('#QuoteForm_PickupLocation').fill('Daytona, FL');
    await form.locator('#QuoteForm_DeliveryLocation').fill('Atlanta, GA');
    await form.locator('#QuoteForm_FreightType').fill('Dry van');
    await form.getByRole('button', { name: 'Submit Request' }).click();

    // Native HTML5 "required" validation should block the request client-side.
    await expect(page).toHaveURL('/');
    await expect(page.getByText(/quote request has been submitted successfully/i)).not.toBeVisible();

    await loginAsAdmin(page, '/Admin/QuoteRequests');
    await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.getByText('No quote requests found.')).toBeVisible();
  });
});
