using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{

    class LoginPage
    {
        IWebDriver driver;
        JObject jObject;
        public List<string> screenshotList = new List<string>();



        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("LoginPageSelector.json");

        }

        /// <summary>
        /// To Verify that user is able to login to the application
        /// </summary>
        /// <param name="inputjson">The input json</param>
        /// <returns>Test reports</returns>
        public List<TestReportSteps> LoginToApplication(JToken inputjson)
        {
            List<TestReportSteps> listOfReport = new List<TestReportSteps>();
            int step = 0;
            try
            {
                listOfReport.Add(ReusableComponents.GenerateReportSteps("Enter username", "username", "To Verify that user is able to login to the application", step));
                ReusableComponents.SendKeys(driver, "XPath", jObject["username"].ToString(), inputjson["username"].ToString());
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Enter password", "password", "To Verify that user is able to login to the application", step));
                ReusableComponents.SendKeys(driver, "XPath", jObject["password"].ToString(), inputjson["password"].ToString());
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Capture screenshot", "details", "To Verify that user is able to login to the application", step));
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "details")); ;
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Click loginButton", "", "To Verify that user is able to login to the application", step));
                ReusableComponents.Click(driver, "XPath", jObject["loginButton"].ToString());
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Wait until verifyLogin is visible", "", "To Verify that user is able to login to the application", step));
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject["verifyLogin"].ToString());
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Wait until loader is invisible", "", "To Verify that user is able to login to the application", step));
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", jObject["loader"].ToString());
                listOfReport[step].actualResultFail = "";
                step++;

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Capture screenshot", "verifyLogin", "To Verify that user is able to login to the application", step));
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "verifyLogin")); ;
                listOfReport[step].actualResultFail = "";
                step++;
            }
            catch (Exception e)
            {
                Console.WriteLine("LoginToApplication failed: " + e);
                listOfReport[step - 1].SetActualResultFail("Failed");
            }
            return listOfReport;
        }



        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoginPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}