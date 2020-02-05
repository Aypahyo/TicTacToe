using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.WPFFrontend;
using TicTacToe.WPFFrontend.GameService;

namespace TicTacToe.WPFFrontendTest.units.WindowViewModelTests
{
    [TestFixture]
    public class WindowViewModelFixture
    {
        private WindowViewModel _uut;
        private Mock<IGameService> gameServiceMock;

        [SetUp]
        public void SetUp()
        {
            using var mock = AutoMock.GetLoose();
            gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(m => m.TryMove(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>())).Returns(Task.FromResult(true));
            gameServiceMock.Setup(m => m.TryNewGame(It.IsAny<string>())).Returns(Task.FromResult(true));
            mock.Provide(gameServiceMock.Object);
            _uut = mock.Create<WindowViewModel>();
        }

        [TestCaseSource(typeof(WindowViewModelData), nameof(WindowViewModelData.MapSetCaseData))]
        public void RaiseOnMapSet(MapSetCase data)
        {
            var args = new List<PropertyChangedEventArgs>();
            _uut.PropertyChanged += (s, e) => args.Add(e);

            foreach (var change in data.changes)
                _uut.SetMap(change.Item1, change.Item2, change.Item3);

            Assert.AreEqual(
                data.ExpectedRaiseCount,
                args
                    .Where(x => x.PropertyName == nameof(_uut.MAP))
                    .Count());
        }

        [TestCaseSource(typeof(WindowViewModelData), nameof(WindowViewModelData.MapClickData))]
        public void RaiseOnMapClick(MapClickCase data)
        {
            var args = new List<PropertyChangedEventArgs>();
            _uut.PropertyChanged += (s, e) => args.Add(e);

            foreach (var arg in data.args)
                _uut.MapClick.Execute(arg);

            Assert.AreEqual(
                data.ExpectedRaiseCount,
                args
                    .Where(x => x.PropertyName == nameof(_uut.MAP))
                    .Count());
        }


        [TestCaseSource(typeof(WindowViewModelData), nameof(WindowViewModelData.MapSetCaseData))]
        public void RaiseOnMapClear(MapSetCase data)
        {
            var args = new List<PropertyChangedEventArgs>();
            _uut.PropertyChanged += (s, e) => args.Add(e);

            foreach (var change in data.changes)
                _uut.SetMap(change.Item1, change.Item2, change.Item3);

            args.Clear();

            _uut.ClearMap();

            Assert.AreEqual(
                data.MapChanged ? 1 : 0,
                args
                    .Where(x => x.PropertyName == nameof(_uut.MAP))
                    .Count());
        }


        [TestCase("status", 1)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void RaiseOnSystemstateSet(string status, int raisecount)
        {
            var args = new List<PropertyChangedEventArgs>();
            _uut.PropertyChanged += (s, e) => args.Add(e);

            _uut.Systemstate = status;

            Assert.AreEqual( 
                raisecount,
                args
                    .Where(x => x.PropertyName == nameof(_uut.Systemstate))
                    .Count());
        }
    
        [TestCase("H,H", "   , X ,   ", "   ,   ,   ")]
        [TestCase("C,H", "   , X ,   ", "   ,   ,   ")]
        [TestCase("H,C", "   , X ,   ", "   ,   ,   ")]
        [TestCase("C,C", "   , X ,   ", "   ,   ,   ")]
        [TestCase(null, "   , X ,   ", "   , X ,   ")]
        [TestCase("H", "   , X ,   ", "   , X ,   ")]
        [TestCase("foo", "   , X ,   ", "   , X ,   ")]
        public void ControlClickClearsMap(string control, string startmap, string expected)
        {
            WindowViewModelHelper.ImportMap(_uut, startmap);
            _uut.ControlClick.Execute(control);
            var actual = WindowViewModelHelper.ExportMap(_uut);
            Assert.AreEqual(expected, actual);
        }
    
        [Test]
        public void GetsPlayerFromIGameService()
        {
            gameServiceMock.Setup(m => m.GetCurrentSymbol()).Returns(Task.FromResult('A'));
            _uut.MapClick.Execute("1,1");
            var map = WindowViewModelHelper.ExportMap(_uut);
            Assert.AreEqual("   , A ,   ", map);
        }

        [Test]
        public void GetStatusFromIGameService()
        {
            string expected = "this is a status";
            gameServiceMock.Raise(m => m.GameStatus += null, new StatusEventArgs() { Status = expected });
            Assert.AreEqual(expected, _uut.Systemstate);
        }

        [Test]
        public void MoveIsSentToGameService()
        {
            var expected = new List<Tuple<int, int, char>> { 
                new Tuple<int, int, char>(1, 2, 'F')
            };
            var actual = new List<Tuple<int, int, char>>();
            gameServiceMock
                .Setup(m => m.TryMove(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>()))
                .Callback<int, int, char>((col, row, c) => { actual.Add(new Tuple<int, int, char>(col, row, c)); })
                .Returns(Task.FromResult(true));
            _uut.SetMap(expected[0].Item1, expected[0].Item2, expected[0].Item3);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MoveIsDeniedByGameService()
        {
            gameServiceMock
                .Setup(m => m.TryMove(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<char>()))
                .Returns(Task.FromResult(false));
            _uut.SetMap(1, 2, 'D');
            string map = WindowViewModelHelper.ExportMap(_uut);
            Assert.AreEqual("   ,   ,   ", map);
        }

        [Test]
        public void NewGameIsSentToGameService()
        {
            var expected = new List<string> { "H,H" };
            var actual = new List<string>();
            gameServiceMock
                .Setup(m => m.TryNewGame(It.IsAny<string>()))
                .Callback<string>((str) => { actual.Add(str); })
                .Returns(Task.FromResult(true));

            _uut.ControlClick.Execute(expected[0]);
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void NewGameIsDeneidByGameService()
        {
            gameServiceMock
                .Setup(m => m.TryNewGame(It.IsAny<string>()))
                .Returns(Task.FromResult(false));
            _uut.ControlClick.Execute("H,H");
            string map = WindowViewModelHelper.ExportMap(_uut);
            Assert.AreEqual("   ,   ,   ", map);
        }
    }
}
