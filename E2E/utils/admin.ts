import { Page, expect } from '@playwright/test';
import { config } from '../config';

/**
 * Logs in through the real Identity login form. Navigates to `returnPath` first so
 * the app's own redirect-to-login-then-back-to-returnUrl flow is exercised, the same
 * way a real admin would land there after being redirected.
 */
export async function loginAsAdmin(page: Page, returnPath = '/Admin/Dashboard') {
  await page.goto(returnPath);
  await expect(page).toHaveURL(/\/Identity\/Account\/Login/);

  await page.getByLabel('Email').fill(config.adminEmail);
  await page.getByLabel('Password').fill(config.adminPassword);
  await page.getByRole('button', { name: 'Log in' }).click();

  await expect(page).toHaveURL(new RegExp(escapeForRegex(returnPath)));
}

export async function deleteQuoteRequestByName(page: Page, fullName: string) {
  await page.goto(`/Admin/QuoteRequests?search=${encodeURIComponent(fullName)}`);
  const row = page.locator('table.table tbody tr', { hasText: fullName });
  if ((await row.count()) === 0) {
    return;
  }
  page.once('dialog', (dialog) => dialog.accept());
  await row.getByRole('button', { name: 'Delete' }).click();
  await page.waitForURL(/\/Admin\/QuoteRequests/);
}

export async function deleteContactMessageByName(page: Page, name: string) {
  await page.goto(`/Admin/ContactMessages?search=${encodeURIComponent(name)}`);
  const row = page.locator('table.table tbody tr', { hasText: name });
  if ((await row.count()) === 0) {
    return;
  }
  page.once('dialog', (dialog) => dialog.accept());
  await row.getByRole('button', { name: 'Delete' }).click();
  await page.waitForURL(/\/Admin\/ContactMessages/);
}

function escapeForRegex(value: string): string {
  return value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}
