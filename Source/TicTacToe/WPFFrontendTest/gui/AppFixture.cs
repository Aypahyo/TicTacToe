using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.WPFFrontend;

namespace TicTacToe.WPFFrontendTest.gui
{
    [TestFixture]
    class AppFixture
    {
        private UIA3Automation automation;
        private Window window;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            automation = new UIA3Automation();
            window = GuiSetUpFixture.App.GetMainWindow(automation);
        }

        [Test]
        public void CanClickAllMapOptions()
        {
            var clicksForATie = new[] {
                    "map00", "map02", "map01",
                    "map10", "map11", "map21",
                    "map12", "map22", "map20"
            };

            foreach (var buttonName in clicksForATie)
            {
                ClickMapButton(buttonName);
            }
            
            int actual = GetMapTextBlocks()
              .Count(c => !string.IsNullOrWhiteSpace(c.Name));

            Assert.AreEqual(clicksForATie.Length, actual);
        }

        private IEnumerable<AutomationElement> GetMapTextBlocks()
        {
            return MapButtons()
              .Select(b => b.FindFirstDescendant(cf => cf.ByClassName("TextBlock")));
        }

        private void ClickMapButton(string buttonName)
        {
            MapButtons()
                .FirstOrDefault(b => b.AutomationId == buttonName)
                ?.Invoke();
        }

        private Button[] MapButtons()
        {
            return AllButtons()
                .Where(b => b.AutomationId.StartsWith("map"))
                .ToArray();
        }

        private Button[] AllButtons()
        {
            return window
                .FindAllDescendants(cf => cf.ByClassName(nameof(Button)))
                .Select(b => b.AsButton())
                .ToArray();
        }
    }
}
