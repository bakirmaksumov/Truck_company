import { test, expect } from '@playwright/test';

test.describe('Public routes and service responses', () => {
  test('health endpoint returns healthy JSON', async ({ request }) => {
    const response = await request.get('/health');
    expect(response.ok()).toBeTruthy();
    await expect(response.json()).resolves.toEqual({ status: 'healthy' });
  });

  test('privacy page is reachable', async ({ page }) => {
    await page.goto('/Home/Privacy');
    await expect(page).toHaveURL(/\/Home\/Privacy$/);
    await expect(page.getByRole('heading', { name: /privacy/i })).toBeVisible();
  });

  test('sitemap returns valid XML with the home URL', async ({ request }) => {
    const response = await request.get('/sitemap.xml');
    expect(response.ok()).toBeTruthy();
    expect(response.headers()['content-type']).toContain('application/xml');
    const body = await response.text();
    expect(body).toContain('<urlset');
    expect(body).toContain('<loc>');
  });

  test('unknown route returns not found', async ({ request }) => {
    const response = await request.get(`/route-that-does-not-exist-${Date.now()}`);
    expect(response.status()).toBe(404);
  });
});
