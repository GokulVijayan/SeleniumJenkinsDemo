

using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestScript
{
    class LoginTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        public LoginTest()
        {
            VerifyLogin();
        }

        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            testObjective = "To Verify that user is able to login to the landtrack application.";
            scriptName = "TS001_Landtrack_Login to Application";
            jsonArray = ConfigFile.RetrieveInputTestData("LoginTest.json");
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void VerifyLogin()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
                    report = loginPage.LoginToApplication(testData);
                    foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);


                    Exit();
                }
            }
        }

        [TearDown]
        public void Exit()
        {
            driver.Quit();
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
        }

    }
}
