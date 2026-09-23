import { test, expect } from '@playwright/test';
import { loginAsAdmin, deleteContactMessageByName } from '../utils/admin';

test.describe('Admin - Contact Messages', () => {
  test('admin can find a submitted message and delete it', async ({ page }) => {
    const uniqueName = `E2E Admin Contact ${Date.now()}`;

    await page.goto('/');
    const form = page.locator('form.contact-form');
    await form.locator('#ContactForm_Name').fill(uniqueName);
    await form.locator('#ContactForm_Email').fill('e2e-admin-contact@example.com');
    await form.locator('#ContactForm_Phone').fill('+1 386 555 6666');
    await form.locator('#ContactForm_Message').fill('E2E admin test message, please ignore.');
    await Promise.all([
      page.waitForResponse((response) => response.url().endsWith('/Home/SubmitContact') && response.request().method() === 'POST'),
      form.getByRole('button', { name: 'Send Message' }).click(),
    ]);

    await loginAsAdmin(page, '/Admin/ContactMessages');
    await page.goto(`/Admin/ContactMessages?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.locator('table.table tbody tr', { hasText: uniqueName })).toBeVisible();

    await deleteContactMessageByName(page, uniqueName);

    await page.goto(`/Admin/ContactMessages?search=${encodeURIComponent(uniqueName)}`);
    await expect(page.locator('table.table tbody tr', { hasText: uniqueName })).toHaveCount(0);
  });
});
