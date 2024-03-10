// See https://aka.ms/new-console-template for more information
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.Drawing;
using GenericSeleniumTool;
using CsvHelper;
using System.Globalization;
//using https://github.com/tonerdo/dotnet-env

namespace Test
{

	[TestFixture]
	public class Tests
        {
		IWebDriver driver = new Core().driver;
                Core lib = new Core();
                Subroutines sr = new Subroutines();
		CSV_Parsing csvp = new CSV_Parsing();
                //field initializers cannot reference non static field
                //instead reference a new sub object in place at initialization

                //set up pre-initialized test-scope variables
                string screenSize;
		

		[SetUp]
                public void SetUp()
                {
                        //load dictionaries

			string driverName = "";
			if (OperatingSystem.IsLinux()) 
			{
				driverName = "chromedriver_linux64";
			}
			else if (OperatingSystem.IsMacOS())
			{
				driverName = "chromedriver_mac_arm64";
			}
                        driver = new ChromeDriver(driverName);
                        //environment variables
                        DotNetEnv.Env.Load();
			screenSize = DotNetEnv.Env.GetString("SCREENSIZE");
                        //URL to go to
                        string URL = "https:// .com/";
                        driver.Navigate().GoToUrl(URL);
                        //select screen size for driver
                        if (screenSize == "1080") //if screen is too small some elements may not be accessable 
                        {
                                driver.Manage().Window.Size = lib.screenSizes[1080];
                        }
                        else if (screenSize == "1440")
                        {
                                driver.Manage().Window.Size = lib.screenSizes[1440];
                        }
                        else
                        {
                                driver.Manage().Window.Size = lib.screenSizes[1080];
                                //default to 1080 if not set
                        }
                        //set the subobj ref to local chromedriver
                }

		[TearDown]
                public void TearDown()
                {
                        driver.Quit();
                }
		
		[Test]
                public void Test()
                {
			string csvPath = "pathnamehere.csv";
			csvp.CSV_Parse(csvPath, driver);
		}

		static void Main()
                {
                        Tests newTest = new Tests();
                        newTest.SetUp();
                        newTest.Test();
                        newTest.TearDown();
                }
	}
}
