using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;

namespace NDA2CSVTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                using (var bsda = BSDAApp.Create("http://127.0.0.1:4723", @"D:\Tools\NewWare BSTDA\BTSDA.exe"))
                {
                    
                    //System.Threading.Thread.Sleep(5000);
                    bsda.OpenFile(@"D:\Temp\201125t05-067.nda");
                    bsda.ExportCSV($@"D:\Tools\NewWare BSTDA\out{i}.csv");
                }
            }
            
            
        }
    }
}
