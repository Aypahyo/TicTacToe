using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.WPFFrontend.GameService
{
    public class NoGameService : IGameService
    {
        public event EventHandler<StatusEventArgs> GameStatus = delegate{ };

        public Task<char> GetCurrentSymbol()
        {
            GameStatus(this, new StatusEventArgs { SystemState = nameof(GetCurrentSymbol) });
            return Task.FromResult(
                new char[] { 'X', 'O', 'A', 'B', 'C' }
                [new Random().Next(0, 4)]);
        }

        public Task<bool> TryMove(int col, int row, char symbol)
        {
            GameStatus(this, new StatusEventArgs { SystemState = nameof(TryMove) });
            return Task.FromResult(true);
        }

        public Task<bool> TryNewGame(string gameType)
        {
            GameStatus(this, new StatusEventArgs { SystemState = nameof(TryNewGame) });
            return Task.FromResult(true);
        }
    }
}
