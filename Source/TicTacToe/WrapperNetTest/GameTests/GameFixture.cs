using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
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
            Assert.IsTrue(uut.Move(new GameMove(1, 1, 'X')));
        }

        [Test]
        public void GameStatusEvent()
        {
            int count = 0;
            uut.GameStatusChanged += (s, e) => { ++count; };
            uut.Move(1, 1, 'X');
            Assert.AreEqual(1, count, "one change, one event");
        }

        [Test]
        public void GameStatusEventHasValues()
        {
            String stateJson = null;
            uut.GameStatusChanged += (s, e) => {
                stateJson = Regex.Replace(e, @"\s+", " ").Trim();
            };
            var expected = new GameMove(1, 1, 'X');
            uut.Move(expected);
            dynamic state = JsonConvert.DeserializeObject(stateJson);
            char nextPlayer = state.NextPlayer;
            GameMove actual = JsonConvert.DeserializeObject<GameMove> (state.Moves[0].ToString());
            Assert.AreEqual(1, state.Moves.Count, $"{stateJson}");
            Assert.AreEqual($"{expected}", $"{actual}", $"{stateJson}");
            Assert.AreEqual('O', nextPlayer, $"{stateJson}");
        }
    }
}
