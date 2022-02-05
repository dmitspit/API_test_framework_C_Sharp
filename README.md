This is API Testing Framework using .Net Framework 4.8 with RestSharp library.
========================
### In my project I used:
* .Net Framework 4.8
* NUnit 
* RestSharp library
* Newtonjson library
* Bogus library (to create random values)
* Log4net
* Allure report
---
## Follow the instruction to see hot my framework works 
### 1) Run the test server
- ** Download and install (npm) Json server https://github.com/typicode/json-server

- ** In order to install the JSON server, use the following command:

npm install -g json-server-auth

- ** Start the JSON server with the following command:

json-server-auth http://jsonplaceholder.typicode.com/db

JSON server should be running on http://localhost:3000 now.
---
### 2) Run tests from Visual studio
---
### 3) To get Allure report
 1) To run report use command  - allure serve <Your dirctory to Allure-Results folder> (it automatically opens your browser with actual report)
 2) To clean results use command - allure generate --clean --output <your-result-folder> 
 3) Additional inf : To generate the report from existing Allure results you can use the following command:
    allure generate <directory-with-results> -o <directory-with-report>
    *- When the report is generated you can open it in your default system browser. Simply run
    When the report is generated you can open it in your default system browser. Simply run
