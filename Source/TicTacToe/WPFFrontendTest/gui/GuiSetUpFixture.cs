using FlaUI.Core;
using FlaUI.UIA3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace TicTacToe.WPFFrontendTest.gui
{
    [SetUpFixture]
    public class GuiSetUpFixture
    {
        public static Application App;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            foreach(var p in Process.GetProcessesByName("WPFFrontend")) { p.Kill(); }

            var path = 
                Path.GetFullPath(
                Path.Combine(
                    TestContext.CurrentContext.TestDirectory, 
                    "../../../..", 
                    @"WPFFrontend\bin\Debug\netcoreapp3.1", 
                    "WPFFrontend.exe"));
            FileAssert.Exists(path);
            App = Application.Launch(path);

            while (Process.GetProcessesByName("WPFFrontend").Length == 0) ;
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        { 
            App.Close();
            App.Dispose();
            App.Kill();
        }
    }
}
