using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeNet;

namespace WrapperNetTest.GameTests
{
    [TestFixture]
    public class GameFixture
    {
        private Game uut;

        [SetUp]
        public void SetUp()
        {
            uut = new Game();
        }

        [Test]
        public void CurrentPlayerTest()
        {
            Assert.AreEqual('X', uut.CurrentPlayer());
        }

        [Test]
        public void Move()
        {
            Assert.IsTrue(uut.Move(1, 1, 'X'));
        }

        [Test]
        public void GameMove()
        {
            var move = new GameMove(1, 1, 'X')
            Assert.IsTrue(uut.Move(move));
        }

    }
}
