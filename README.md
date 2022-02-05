This is API Testing Framework using .Net Framework 4.8 with RestSharp library.
========================
### In my project I used:
* .Net Framework 4.8
* NUnit 
* RestSharp library
* Newtonjson library
* Bogus library (to create random values)
* Log4net
---
## Follow the instruction to see hot my framework works, 
**download and install (npm) Json server https://github.com/typicode/json-server

In order to install the JSON server, use the following command:

npm install -g json-server-auth

Start the JSON server with the following command:

json-server-auth http://jsonplaceholder.typicode.com/db

JSON server should be running on http://localhost:3000 now.

###############
# Allure
 1) Install Allure (NuGet)
 1.1) Add configure files(allureConfig.json , categories.json) to bin dir 
 2) To run report use command  - allure serve <Your dirctory to Allure-Results folder>
 3) To clean results use command - allure generate --clean --output <your-result-folder> 
 4) Additional inf : To generate the report from existing Allure results you can use the following command:
    allure generate <directory-with-results> -o <directory-with-report>
    *- When the report is generated you can open it in your default system browser. Simply run
    When the report is generated you can open it in your default system browser. Simply run
