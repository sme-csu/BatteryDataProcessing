using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NDA2CSVTest
{
    public static class ElementHelper
    {
        public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan DefaultCheckInterval = TimeSpan.FromMilliseconds(100);
        public static AppiumWebElement Click(this AppiumWebElement source, string xpath, TimeSpan? timeout, TimeSpan? interval)
        {
            if (CheckControlAvailable(source,xpath,timeout,interval))
            {
                var button = source.FindElementByXPath(xpath);
                button.Click();
                return button;
            }
            return null;
        }

        public static AppiumWebElement Click(this AppiumWebElement source,string xpath)
        {
            return Click(source, xpath, null, null);
        }

        public static AppiumWebElement SendKeys(this AppiumWebElement source, string text, string xpath, TimeSpan? timeout, TimeSpan? interval)
        {
            if (CheckControlAvailable(source, xpath, timeout, interval))
            {
                var edit = source.FindElementByXPath(xpath);
                edit.SendKeys(text);
                return edit;
            }
            return null;
        }

        public static AppiumWebElement SendKeys(this AppiumWebElement source,string text,string xpath)
        {
            return source.SendKeys(text, xpath, null, null);
        }

        public static bool CheckControlAvailable(this AppiumWebElement source, string xpath,TimeSpan? timeout, TimeSpan? interval)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed<(timeout??DefaultTimeOut))
            {
                try
                {
                    source.FindElementByXPath(xpath);
                    return true;
                }
                catch (WebDriverException)
                {
                }
                catch (Exception)
                {
                    throw;
                }
                System.Threading.Thread.Sleep((int)(interval ?? DefaultCheckInterval).TotalMilliseconds);
            }
            return false;
            
        }
    }
}
