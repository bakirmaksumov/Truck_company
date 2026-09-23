import path from 'node:path';
import { test, expect } from '@playwright/test';
import { loginAsAdmin } from '../utils/admin';

test.describe('Admin - Services CRUD and image upload', () => {
  test('admin can create, edit, upload an image, and delete a service', async ({ page }) => {
    const title = `E2E Service ${Date.now()}`;
    const editedTitle = `${title} Edited`;
    const imagePath = path.resolve('..', 'wwwroot', 'images', 'truck.png');

    await loginAsAdmin(page, '/Admin/Services/Create');
    await page.locator('input[name="Title"]').fill(title);
    await page.locator('textarea[name="ShortDescription"]').fill('E2E service short description');
    await page.locator('textarea[name="FullDescription"]').fill('E2E service full description');
    await page.locator('input[name="Icon"]').fill('truck');
    await page.locator('input[name="ImageUrl"]').fill('/images/truck.png');
    await page.locator('input[name="DisplayOrder"]').fill('99');
    await page.locator('select[name="IsActive"]').selectOption('true');
    await page.getByRole('button', { name: 'Save' }).click();

    await expect(page).toHaveURL(/\/Admin\/Services$/);
    const row = page.locator('table.table tbody tr', { hasText: title });
    await expect(row).toBeVisible();

    await row.getByRole('link', { name: 'Edit' }).click();
    await expect(page.getByRole('heading', { name: 'Edit Service' })).toBeVisible();
    await page.locator('input[name="Title"]').fill(editedTitle);
    await page.locator('input[type="file"][name="imageFile"]').setInputFiles(imagePath);
    await page.getByRole('button', { name: 'Save Changes' }).click();

    await expect(page).toHaveURL(/\/Admin\/Services$/);
    await expect(page.locator('table.table tbody tr', { hasText: editedTitle })).toBeVisible();

    const editedRow = page.locator('table.table tbody tr', { hasText: editedTitle });
    page.once('dialog', (dialog) => dialog.accept());
    await editedRow.getByRole('button', { name: 'Delete' }).click();
    await page.waitForURL(/\/Admin\/Services$/);
    await expect(page.locator('table.table tbody tr', { hasText: editedTitle })).toHaveCount(0);
  });
});
