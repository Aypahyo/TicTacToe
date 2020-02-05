using NUnit.Framework;
using System;
using System.Windows.Input;
using TicTacToe.WPFFrontend;

namespace TicTacToe.WPFFrontendTest.units.CSVCommandTests
{
    [TestFixture]
    class CSVCommandFixture
    {
        ICommand _uut;
        Action<string[]> _executable;

        [SetUp]
        public void SetUp()
        {
            _uut = new CSVCommand(args => _executable(args));
            _executable = args => { };
        }

        [Test]
        public void MissingArgsNoExecution()
        {
            int calls = 0;
            _executable = args => { ++calls; };
            _uut.Execute(null);
        }

        [Test]
        public void NoStringNoExecution()
        {
            int calls = 0;
            _executable = args => { ++calls; };
            _uut.Execute(new object());
        }

        [TestCase("", 0, null, Description = "non null empty args")]
        [TestCase("one", 1, "one")]
        [TestCase("one , , , , , ", 1, "one")]
        [TestCase("one,two", 2, "one,two")]
        [TestCase(" one  , two,", 2, "one,two")]
        [TestCase("one ,two , three  ", 3, "one,two,three")]
        public void CsvArgsAreSplitToArray(object parameter, int length, string csvexpected)
        {
            string[] actual = null;
            int calls = 0;
            var expected = csvexpected?.Split(',') ?? new string[0];
            _executable = args => { actual = args; ++calls; };
            _uut.Execute(parameter);
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(length, actual.Length);
            Assert.AreEqual(1, calls, "this test is not about parameter validation, call should happen");
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase(42, ExpectedResult = false)]
        [TestCase("", ExpectedResult = true)]
        [TestCase("foo", ExpectedResult = true)]
        [TestCase("foo,bar", ExpectedResult = true)]
        public bool CsvArgsCanExecute(object parameter)
        {
            return _uut.CanExecute(parameter); 
        }
    }
}
