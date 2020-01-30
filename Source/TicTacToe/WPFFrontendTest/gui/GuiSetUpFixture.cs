using FlaUI.Core;
using FlaUI.UIA3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.WPFFrontendTest.gui
{
    [SetUpFixture]
    public class GuiSetUpFixture
    {
        public static Application App;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            App = Application.Launch("WPFFrontend.exe");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        { 
            App.Close();
            App.Dispose();
            App.Kill();
            using var automation = new UIA3Automation();
            App.GetMainWindow(automation).Close();
        }
    }
}
