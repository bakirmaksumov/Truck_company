import { test, expect } from '@playwright/test';

const viewports = [
  { name: 'mobile', width: 375, height: 667 },
  { name: 'tablet', width: 768, height: 1024 },
  { name: 'desktop', width: 1440, height: 900 },
] as const;

test.describe('Responsive public site', () => {
  for (const viewport of viewports) {
    test(`${viewport.name} layout has no horizontal overflow and keeps forms usable`, async ({ page }) => {
      await page.setViewportSize({ width: viewport.width, height: viewport.height });
      await page.goto('/');

      await expect(page.locator('meta[name="viewport"]')).toHaveAttribute(
        'content',
        /width=device-width/,
      );
      await expect(page.locator('form.contact-form')).toBeVisible();
      await expect(page.locator('form.quote-form')).toBeVisible();
      await expect(page.locator('form.quote-form button[type="submit"]')).toBeVisible();

      const pageWidth = await page.evaluate(() => document.documentElement.scrollWidth);
      expect(pageWidth, `${viewport.name} page overflows horizontally`).toBeLessThanOrEqual(viewport.width);
    });
  }

  test('mobile navigation opens and closes without leaving the viewport', async ({ page }) => {
    await page.setViewportSize({ width: 375, height: 667 });
    await page.goto('/');

    const navigation = page.locator('.main-nav');
    const toggle = page.locator('.nav-toggle');
    await expect(toggle).toBeVisible();
    await expect(navigation).toBeHidden();

    await toggle.click();
    await expect(navigation).toBeVisible();
    await expect(navigation.getByRole('link', { name: 'Services' })).toBeVisible();

    await navigation.getByRole('link', { name: 'Services' }).click();
    await expect(navigation).toBeHidden();
  });

  test('desktop navigation is visible and mobile toggle is hidden', async ({ page }) => {
    await page.setViewportSize({ width: 1440, height: 900 });
    await page.goto('/');

    await expect(page.locator('.main-nav')).toBeVisible();
    await expect(page.locator('.nav-toggle')).toBeHidden();
    await expect(page.getByRole('link', { name: 'Get a Quote' })).toBeVisible();
  });
});
