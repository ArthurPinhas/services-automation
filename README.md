---

# Services Automation

## Overview

The Services Automation project aims to automate the checkup and certain validations or functions of services using the NUnit framework along with Selenium. Each flow of test will be tailored for different services based on their specific requirements.

As an initial stage, this project automates an NUnit test that connects to a Portainer account and checks if the containers are up.

## Features

- **Automated Checkup**: Utilizes the NUnit framework and Selenium to automate the checkup process for container services.

## Dependencies

- [NUnit](https://www.nunit.org/): A unit-testing framework for all .NET languages.
- [Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/): A tool used for automating web applications for testing purposes.
- [WebDriverManager](https://github.com/rosolko/WebDriverManager.Net): A .NET library for managing WebDriver binaries automatically.
- [WebDriverManager Chrome Driver](https://github.com/rosolko/WebDriverManager.Net): WebDriverManager configuration for ChromeDriver.
- [Selenium Support](https://www.selenium.dev/documentation/en/selenium_support/): Support classes and methods for Selenium WebDriver.
- [Selenium Extras WaitHelpers](https://www.selenium.dev/selenium/docs/api/dotnet/html/N_SeleniumExtras_WaitHelpers.htm): Additional wait helpers for Selenium WebDriver.

## Setup

To run the automated tests, follow these steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/services-automation.git
   ```

2. Navigate to the project directory:

   ```bash
   cd services-automation
   ```

3. Create your own `appsettings.pageobjects.json` file containing the URLs and credentials for your services.

4. Install the necessary dependencies (ensure you have NUnit, Selenium WebDriver, WebDriverManager, Selenium Support, and Selenium Extras WaitHelpers installed):

   ```bash
   # Replace with your package manager command
   dotnet add package NUnit
   dotnet add package Selenium.WebDriver
   dotnet add package WebDriverManager
   dotnet add package Selenium.Support
   dotnet add package SeleniumExtras.WaitHelpers
   ```

5. Run the NUnit tests:

   ```bash
   dotnet test
   ```

## Contributions

Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/new-feature`).
3. Make your changes and commit them (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/new-feature`).
5. Create a new Pull Request.

## License

This project is licensed under the [MIT License](LICENSE).

---
