using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace NDA2CSVTest
{
    
    class BSDAApp:IDisposable
    {
        private WindowsDriver<WindowsElement> session;
        private WindowsElement rootElement;
        private BSDAApp(WindowsDriver<WindowsElement> session)
        {
            this.session = session;
            rootElement = session.FindElementByXPath("/Window");
        }

        public void Dispose()
        {
            session?.Close();
        }

        public void OpenFile(string  path)
        {
            var menu=rootElement.Click("/Window/MenuBar/MenuItem[1]");
            //var fileMenu=session.FindElementByName("文件(F)");
            //fileMenu.Click();
            //System.Threading.Thread.Sleep(1000);
            //var b=fileMenu.CheckControlAvailable("//MenuItem[1]", null,null);
            menu.SendKeys(Keys.ArrowDown);
            menu.SendKeys(Keys.Enter);
            //var openMenu = menu.FindElementByXPath("//MenuItem[1]");
            //openMenu.Click();
            
            var filePathTextBox = session.FindElementByXPath("/Window/Window/ComboBox/Edit[@ClassName=\"Edit\"][@Name=\"File name:\"]");
            filePathTextBox.SendKeys(path + Keys.Enter);

        }
        public void ExportCSV(string path)
        {
            if (System.IO.File.Exists(path))
            {
                File.Delete(path);
            }
            var dirname=System.IO.Path.GetDirectoryName(path);
            var fname = System.IO.Path.GetFileName(path);
            rootElement.Click("/Window/Pane[@ClassName=\"Afx:DockPane:400000:8:10003:10\"]/Pane[4]/Button");
            //var b=session.FindElementByXPath("/Window/Pane[@ClassName=\"Afx:DockPane:400000:8:10003:10\"]/Pane[4]/Button");
            //b.Click();
            var exportWindow = session.FindElementByName("导出");
            exportWindow.Click("/Window/RadioButton[@ClassName=\"Button\"][@Name=\"常规报表\"]");
            //var normalReport = exportWindow.FindElementByXPath("/Window/RadioButton[@ClassName=\"Button\"][@Name=\"常规报表\"]");
            //normalReport.Click();
            exportWindow.SendKeys(dirname, "/Window/ComboBox/Edit");
            //var exportPath = exportWindow.FindElementByXPath("/Window/ComboBox/Edit");
            //exportPath.SendKeys(dirname);
            exportWindow.SendKeys(fname, "/Window/ComboBox[2]/Edit");
            //var exportFile = exportWindow.FindElementByXPath("/Window/ComboBox[2]/Edit");
            //exportFile.SendKeys(fname);
            exportWindow.Click("//RadioButton[@Name=\"CSV\"]");
            //var csvformat=exportWindow.FindElementByName("CSV");
            //csvformat.Click();
            exportWindow.Click("/Window/Button[@Name=\"导出\"]");
            //var okbutton = exportWindow.FindElementByXPath("/Window/Button[@Name=\"导出\"]");
            //okbutton.Click();
            //System.Threading.Thread.Sleep(5000);
            exportWindow.Click("/Window/Window/Button[@Name=\"OK\"]");
            //var closebutton = exportWindow.FindElementByXPath("/Window/Window/Button[@Name=\"OK\"]");
            //closebutton.Click();

        }
        public static BSDAApp Create(string winAppDriverUrl, string BSDAPath)
        {
            winAppDriverUrl.RequireNotNull(nameof(winAppDriverUrl));
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", BSDAPath);
            WindowsDriver<WindowsElement> s = new WindowsDriver<WindowsElement>(new Uri(winAppDriverUrl), options);
            return new BSDAApp(s);
        }

    }
}
