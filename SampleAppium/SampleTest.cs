using System;
using System.Net;
using System.Net.Sockets;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SampleAppium
{
    public class SampleTest
    {
        private static IWebDriver _driver;        
        private static string udid = "192.168.68.101:5555";

        [TestCase(TestName = "First Test", Description = "First Test to open Google.com in Android")]
        public void FirstTest()
        {
            _driver.Navigate().GoToUrl("https://www.khmerdev.com/");
            //_driver.FindElementByCssSelector(".color-white > span");
            if (_driver.GetType() == typeof(AndroidDriver<AndroidElement>))
            {
                AndroidDriver<AndroidElement> androidDriver = (AndroidDriver<AndroidElement>)_driver;
                androidDriver.FindElementByCssSelector(".color-white > span");
            }
        }
        [SetUp]
        public void Setup()
        {            
            var appiumOptions = new AppiumOptions();            
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, udid);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel 2 XL");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
            //appiumOptions.AddAdditionalCapability("autoGrantPermissions", true);
            appiumOptions.AddAdditionalCapability("chromedriverExecutable", @"C:\WEBDRIVERS\chromedriver.exe");
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://" + GetLocalIPAddress() + ":" + 4723 + "/wd/hub"), appiumOptions);
        }
        public void TearDown()
        {
            _driver.Quit();
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}
