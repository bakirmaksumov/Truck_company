import path from 'node:path';
import fs from 'node:fs/promises';
import { test, expect } from '@playwright/test';
import { loginAsAdmin } from '../utils/admin';

test.describe('Admin - Website settings and attachments', () => {
  test('admin can save settings and upload a hero image', async ({ page }) => {
    const imagePath = path.resolve('..', 'wwwroot', 'images', 'truck.png');

    await loginAsAdmin(page, '/Admin/Settings');
    const companyName = page.locator('#SiteSettings_CompanyName');
    const originalCompanyName = await companyName.inputValue();
    const originalHeroUrl = await page
      .locator('input[name="heroFile"]')
      .locator('xpath=..')
      .getByText(/Current:/)
      .textContent()
      .then((text) => text?.replace(/^Current:\s*/, '').trim() ?? '');
    const testCompanyName = `E2E Company ${Date.now()}`;
    let uploadedHeroUrl = '';

    try {
      await companyName.fill(testCompanyName);
      await page.locator('input[type="file"][name="heroFile"]').setInputFiles(imagePath);
      await page.getByRole('button', { name: 'Save Changes' }).click();

      await expect(page).toHaveURL(/\/Admin\/Settings$/);
      await expect(page.getByText(/website settings were updated successfully/i)).toBeVisible();
      const uploadedHero = page
        .locator('input[name="heroFile"]')
        .locator('xpath=..')
        .getByText(/Current: \/uploads\//);
      await expect(uploadedHero).toBeVisible();
      uploadedHeroUrl = (await uploadedHero.textContent())?.replace(/^Current:\s*/, '').trim() ?? '';

      await page.goto('/');
      await expect(page.locator('.hero-banner')).toBeVisible();
    } finally {
      await page.goto('/Admin/Settings');
      await companyName.fill(originalCompanyName);
      await page.locator('#HeroSection_BackgroundImageUrl').fill(originalHeroUrl);
      await page.getByRole('button', { name: 'Save Changes' }).click();
      await expect(page).toHaveURL(/\/Admin\/Settings$/);
      if (uploadedHeroUrl.startsWith('/uploads/')) {
        await fs.rm(path.resolve('..', 'wwwroot', uploadedHeroUrl.slice(1)), { force: true });
      }
    }
  });
});
