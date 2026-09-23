import { test, expect } from '@playwright/test';
import { config } from '../config';

test.describe('Admin login', () => {
  test('valid credentials redirect to the dashboard', async ({ page }) => {
    await page.goto('/Admin/Dashboard');
    await expect(page).toHaveURL(/\/Identity\/Account\/Login/);

    await page.getByLabel('Email').fill(config.adminEmail);
    await page.getByLabel('Password').fill(config.adminPassword);
    await page.getByRole('button', { name: 'Log in' }).click();

    await expect(page).toHaveURL(/\/Admin\/Dashboard/);
    await expect(page.getByRole('heading', { name: 'Dashboard' })).toBeVisible();
  });

  test('wrong password shows an error and does not grant access', async ({ page }) => {
    await page.goto('/Identity/Account/Login');
    await page.getByLabel('Email').fill(config.adminEmail);
    await page.getByLabel('Password').fill('WrongPassword123!');
    await page.getByRole('button', { name: 'Log in' }).click();

    await expect(page).toHaveURL(/\/Identity\/Account\/Login/);
    await expect(page.getByText(/invalid login attempt/i)).toBeVisible();
  });

  test('admin can log out and loses access to the dashboard', async ({ page }) => {
    await page.goto('/Admin/Dashboard');
    await page.getByLabel('Email').fill(config.adminEmail);
    await page.getByLabel('Password').fill(config.adminPassword);
    await page.getByRole('button', { name: 'Log in' }).click();
    await expect(page).toHaveURL(/\/Admin\/Dashboard/);

    await page.getByRole('button', { name: 'Logout' }).click();
    await page.goto('/Admin/Dashboard');
    await expect(page).toHaveURL(/\/Identity\/Account\/Login/);
  });
});
