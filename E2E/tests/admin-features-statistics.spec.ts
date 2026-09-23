import { test, expect } from '@playwright/test';
import { loginAsAdmin } from '../utils/admin';

test.describe('Admin - Features CRUD', () => {
  test('admin can create, edit, and delete a feature', async ({ page }) => {
    const title = `E2E Feature ${Date.now()}`;
    const editedTitle = `${title} Edited`;

    await loginAsAdmin(page, '/Admin/Features/Create');
    await page.getByLabel('Title').fill(title);
    await page.getByLabel('Description').fill('E2E feature description');
    await page.getByLabel('Icon').selectOption('shield');
    await page.locator('input[name="DisplayOrder"]').fill('99');
    await page.locator('select[name="IsActive"]').selectOption('true');
    await page.getByRole('button', { name: 'Save' }).click();

    await expect(page).toHaveURL(/\/Admin\/Features$/);
    const row = page.locator('table.table tbody tr', { hasText: title });
    await expect(row).toBeVisible();

    await row.getByRole('link', { name: 'Edit' }).click();
    await page.getByLabel('Title').fill(editedTitle);
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.locator('table.table tbody tr', { hasText: editedTitle })).toBeVisible();

    const editedRow = page.locator('table.table tbody tr', { hasText: editedTitle });
    page.once('dialog', (dialog) => dialog.accept());
    await editedRow.getByRole('button', { name: 'Delete' }).click();
    await page.waitForURL(/\/Admin\/Features$/);
    await expect(page.locator('table.table tbody tr', { hasText: editedTitle })).toHaveCount(0);
  });
});

test.describe('Admin - Statistics CRUD', () => {
  test('admin can create, edit, and delete a statistic', async ({ page }) => {
    const value = `E2E-${Date.now()}`;
    const label = `E2E Statistic ${Date.now()}`;
    const editedLabel = `${label} Edited`;

    await loginAsAdmin(page, '/Admin/Statistics/Create');
    await page.getByLabel('Value').fill(value);
    await page.getByLabel('Label').fill(label);
    await page.getByLabel('Icon').fill('truck');
    await page.locator('input[name="DisplayOrder"]').fill('99');
    await page.locator('select[name="IsActive"]').selectOption('true');
    await page.getByRole('button', { name: 'Save' }).click();

    await expect(page).toHaveURL(/\/Admin\/Statistics$/);
    const row = page.locator('table.table tbody tr', { hasText: label });
    await expect(row).toBeVisible();

    await row.getByRole('link', { name: 'Edit' }).click();
    await page.getByLabel('Label').fill(editedLabel);
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.locator('table.table tbody tr', { hasText: editedLabel })).toBeVisible();

    const editedRow = page.locator('table.table tbody tr', { hasText: editedLabel });
    page.once('dialog', (dialog) => dialog.accept());
    await editedRow.getByRole('button', { name: 'Delete' }).click();
    await page.waitForURL(/\/Admin\/Statistics$/);
    await expect(page.locator('table.table tbody tr', { hasText: editedLabel })).toHaveCount(0);
  });
});
