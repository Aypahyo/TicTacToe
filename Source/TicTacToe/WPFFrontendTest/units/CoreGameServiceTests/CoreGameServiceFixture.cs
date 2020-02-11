using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.WPFFrontend;
using TicTacToe.WPFFrontend.GameService;

namespace TicTacToe.WPFFrontendTest.units.CoreGameServiceTests
{
    [TestFixture]
    class CoreGameServiceFixture
    {
        IGameService uut;

        [SetUp]
        public void Setup()
        {
            uut = new CoreGameService();
        }

        [Test]
        public void NotifiesOnGameStateChange()
        {
            StatusEventArgs args = null;
            uut.GameStatus += (s, e) => { args = e; };
            uut.TryMove(1, 1, 'X').Wait();
            Assert.NotNull(args);
        }

        [Test]
        public void CurrentSymbolUpdates()
        {
            Assert.AreEqual('X', uut.GetCurrentSymbol().Result, "new game stars");
            uut.TryMove(0, 0, 'X').Wait();
            Assert.AreEqual('O', uut.GetCurrentSymbol().Result, "x moved");
            uut.TryMove(1, 0, 'O').Wait();
            uut.TryMove(0, 1, 'X').Wait();
            uut.TryMove(1, 1, 'O').Wait();
            uut.TryMove(0, 2, 'X').Wait();
            Assert.AreEqual(' ', uut.GetCurrentSymbol().Result, "game was won");
        }

        [Test]
        public void WinnerX()
        {
            StatusEventArgs args = null;
            uut.GameStatus += (s, e) => { args = e; };
            uut.TryMove(0, 0, 'X').Wait();
            uut.TryMove(1, 0, 'O').Wait();
            uut.TryMove(0, 1, 'X').Wait();
            uut.TryMove(1, 1, 'O').Wait();
            uut.TryMove(0, 2, 'X').Wait();
            StringAssert.IsMatch("X won", args.SystemState, "state should show X has won");
        }

        [Test]
        public void WinnerO()
        {
            StatusEventArgs args = null;
            uut.GameStatus += (s, e) => { args = e; };
            uut.TryMove(0, 0, 'X').Wait();
            uut.TryMove(1, 0, 'O').Wait();
            uut.TryMove(0, 1, 'X').Wait();
            uut.TryMove(1, 1, 'O').Wait();
            uut.TryMove(2, 2, 'X').Wait();
            uut.TryMove(1, 2, 'O').Wait();
            StringAssert.IsMatch("O won", args.SystemState, "state should show X has won");
        }
    }
}
