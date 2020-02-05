using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.Linq;

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

        [SetUp]
        public void Setup()
        {
            Button("control_HH")?.Invoke();
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
                Button(buttonName)?.Invoke();
            }

            int actual = Buttons()
                .Where(b => b.AutomationId.StartsWith("map"))
                .Select(TextBlock)
                .Count(c => !string.IsNullOrWhiteSpace(c.Name));
            
            Assert.AreEqual(clicksForATie.Length, actual);
        }

        [TestCase("map00")]
        [TestCase("map01")]
        [TestCase("map02")]
        [TestCase("map10")]
        [TestCase("map11")]
        [TestCase("map12")]
        [TestCase("map20")]
        [TestCase("map21")]
        [TestCase("map22")]
        public void ClickActivatesButton(string buttonName)
        {
            Button(buttonName)?.Invoke(); ;
            var text = GetTextBlock(buttonName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(text.Name));
        }

        private AutomationElement TextBlock(Button b)
        {
            return b.FindFirstDescendant(cf => cf.ByClassName("TextBlock"));
        }

        private AutomationElement GetTextBlock(string buttonName)
        {
            return TextBlock(Button(buttonName));
        }


        private Button Button(string buttonName)
        {
            return Buttons().FirstOrDefault(b => b.AutomationId == buttonName);
        }

        private Button[] Buttons()
        {
            return window
                .FindAllDescendants(cf => cf.ByClassName(nameof(Button)))
                .Select(b => b.AsButton())
                .ToArray();
        }
    }
}
