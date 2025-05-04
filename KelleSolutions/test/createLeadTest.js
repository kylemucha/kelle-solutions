const { Builder, By, until } = require('selenium-webdriver');
const assert = require('assert');
const fs = require('fs');

describe('Create Lead Test', function () {
  this.timeout(30000); // Increased timeout for reliability

  let driver;
  let testUser;
  let leadsData;

  before(async () => {
    testUser = JSON.parse(fs.readFileSync('./test/lastCreatedUser.json', 'utf8'));
    driver = await new Builder().forBrowser('chrome').build();
    await driver.manage().window().maximize();
    console.log('Browser launched');

    // Define test data
    leadsData = {
      firstName: 'Penny',
      middleName: 'Stone',
      lastName: 'Philips',
      email: 'PennyStone@gmail.com',
      phone: '121383993',
      country: 'USA',
      stateProvince: 'CA',
      city: 'Sample City',
      zipcode: '12345',
      street: '123 Main St',
      organizationName: 'KELLE',
      organizationTitle: 'Kelle solutions real estate',
      tracking: 'no'
    };
  });

  after(async () => {
    if (driver) {
      await driver.quit();
      console.log('Browser closed');
    }
  });

  it('creates a new Lead successfully', async () => {
    try {
      // Step 1: Login
      await driver.get('http://localhost:5072/Identity/Account/Login/Login');
      console.log('Navigated to login page');

      await driver.wait(until.elementLocated(By.id('Input_EmailOrPhone')), 5000);
      await driver.findElement(By.id('Input_EmailOrPhone')).sendKeys(testUser.email);
      await driver.findElement(By.id('Input_Password')).sendKeys(testUser.password);
      await driver.findElement(By.css('button[type="submit"]')).click();
      console.log('Logged in');

      // Wait for login to complete
      await driver.wait(until.urlContains('Dashboard'), 5000);

      // Step 2: Navigate to Lead page
      await driver.get('http://localhost:5072/Leads/MyLeads');
      console.log('Navigated to My Leads page');

      // Step 3: Click on Add button
      const addButton = await driver.wait(until.elementLocated(By.css('.gradient-button')), 5000);
      await driver.wait(until.elementIsVisible(addButton), 5000);
      await addButton.click();
      console.log('Clicked on Add button');

      // Step 4: Fill out Create Lead form
      await driver.wait(until.elementLocated(By.id('Lead_NameFirst')), 5000);
      
      const formFields = [
        { id: 'Lead_NameFirst', value: leadsData.firstName },
        { id: 'Lead_NameMiddle', value: leadsData.middleName },
        { id: 'Lead_NameLast', value: leadsData.lastName },
        { id: 'Lead_Email', value: leadsData.email },
        { id: 'Lead_Phone', value: leadsData.phone },
        { id: 'Lead_Country', value: leadsData.country },
        { id: 'Lead_StateProvince', value: leadsData.stateProvince },
        { id: 'Lead_City', value: leadsData.city },
        { id: 'Lead_Postal', value: leadsData.zipcode },
        { id: 'Lead_Street', value: leadsData.street },
        { id: 'Lead_OrganizationName', value: leadsData.organizationName },
        { id: 'Lead_OrganizationTitle', value: leadsData.organizationTitle },
        { id: 'Lead_Tracking', value: leadsData.tracking }
      ];

      for (const field of formFields) {
        const element = await driver.findElement(By.id(field.id));
        await element.clear();
        await element.sendKeys(field.value);
        console.log(`Filled ${field.id} with ${field.value}`);
      }

      // Step 5: Submit the form
      const submitButton = await driver.findElement(By.css('.submit-button'));
      await submitButton.click();
      console.log('Submitted Create Lead form');

      // Wait for form submission and redirect
      await driver.wait(until.urlContains('MyLeads'), 5000);
      console.log('Successfully navigated to My Leads page');

      // Step 6: Verify lead appears in the list
      const leadFullName = `${leadsData.firstName} ${leadsData.lastName}`;
      const leadElement = await driver.wait(
        until.elementLocated(By.xpath(`//*[contains(text(), "${leadFullName}")]`)),
        5000
      );
      const isDisplayed = await leadElement.isDisplayed();
      assert.strictEqual(isDisplayed, true, `Lead "${leadFullName}" was not found in the list.`);
      console.log('Lead successfully created and verified in the list');

    } catch (error) {
      console.error('Test failed:', error);
      throw error;
    }
  });

  it('shows validation errors when required fields are missing', async () => {
    try {
      await driver.get('http://localhost:5072/Leads/CreateLead');
      console.log('Navigated to Create Lead page');

      // Wait for form to load
      await driver.wait(until.elementLocated(By.id('Lead_NameFirst')), 5000);

      // Only fill non-required fields
      await driver.findElement(By.id('Lead_Phone')).sendKeys('123-456-7890');
      await driver.findElement(By.id('Lead_Tracking')).sendKeys('No');

      // Submit the form
      const submitButton = await driver.findElement(By.css('.submit-button'));
      await submitButton.click();
      console.log('Submitted incomplete form');

      // Wait for validation messages
      await driver.sleep(1000); // Give time for validation to appear

      // Check for validation messages
      const validationMessages = await driver.findElements(By.css('.field-validation-error'));
      assert.ok(validationMessages.length > 0, 'Expected validation errors, but none were shown');
      
      // Log validation messages
      for (const message of validationMessages) {
        const text = await message.getText();
        console.log('Validation error:', text);
      }

    } catch (error) {
      console.error('Validation test failed:', error);
      throw error;
    }
  });
}); 