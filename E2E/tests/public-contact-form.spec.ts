import { test, expect } from '@playwright/test';
import { loginAsAdmin, deleteContactMessageByName } from '../utils/admin';

test.describe('Public contact form', () => {
  test('valid submission creates a new contact message', async ({ page }) => {
    const uniqueName = `E2E Contact ${Date.now()}`;

    await page.goto('/');
    const form = page.locator('form.contact-form');
    await form.locator('#ContactForm_Name').fill(uniqueName);
    await form.locator('#ContactForm_Email').fill('e2e-contact@example.com');
    await form.locator('#ContactForm_Phone').fill('+1 386 000 1111');
    await form.locator('#ContactForm_Message').fill('E2E test message, please ignore.');
    await Promise.all([
      page.waitForResponse((response) => response.url().endsWith('/Home/SubmitContact') && response.request().method() === 'POST'),
      form.getByRole('button', { name: 'Send Message' }).click(),
    ]);

    await loginAsAdmin(page, '/Admin/ContactMessages');
    await page.goto(`/Admin/ContactMessages?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.locator('table.table tbody tr', { hasText: uniqueName })).toBeVisible();

    await deleteContactMessageByName(page, uniqueName);
  });

  test('missing required field blocks submission and creates no record', async ({ page }) => {
    const uniqueName = `E2E Contact Invalid ${Date.now()}`;

    await page.goto('/');
    const form = page.locator('form.contact-form');
    await form.locator('#ContactForm_Name').fill(uniqueName);
    await form.locator('#ContactForm_Phone').fill('+1 386 000 1111');
    // Email and Message are left empty on purpose - both are required fields.
    await form.getByRole('button', { name: 'Send Message' }).click();

    await expect(page).toHaveURL('/');

    await loginAsAdmin(page, '/Admin/ContactMessages');
    await page.goto(`/Admin/ContactMessages?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.locator('table.table tbody tr', { hasText: uniqueName })).toHaveCount(0);
  });
});
