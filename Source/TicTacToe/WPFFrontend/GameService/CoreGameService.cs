using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.WPFFrontend.GameService
{
    public class CoreGameService : IGameService
    {
        private TicTacToeNet.Game game = new TicTacToeNet.Game();

        public event EventHandler<StatusEventArgs> GameStatus = delegate { };

        public CoreGameService()
        {
            game.GameStatusChanged += Game_GameStatusChanged;
        }
        ~CoreGameService()
        {
            game.GameStatusChanged -= Game_GameStatusChanged;
        }

        private void Game_GameStatusChanged(object sender, string e)
        {
            string sysState = "unknown";
            char[][] map = {
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' } };
            try
            {
                dynamic state = JsonConvert.DeserializeObject(e);
                char winner = state.Winner;
                char player = state.NextPlayer;
                string Board = state.Board;

                if (winner == 'X' || winner == 'O') sysState = $"Player {winner} won the game";
                else if (player == 'X' || player == 'O') sysState = $"Player {player} needs to move";

                for(int row = 0; row < 3; ++row)
                for(int col = 0; col < 3; ++col)
                {
                        map[row][col] = Board[row * 4 + col];
                }
            }
            catch 
            {
                //intentionally empty
            }

            GameStatus(this, new StatusEventArgs { SystemState = sysState, MAP = map });
        }

        public Task<char> GetCurrentSymbol()
        {
            //todo async await
            return Task.FromResult(game.CurrentPlayer());
        }

        public Task<bool> TryMove(int col, int row, char symbol)
        {
            //todo async await
            return Task.FromResult(game.Move(row, col, symbol));
        }

        public Task<bool> TryNewGame(string gameType)
        {
            game.GameStatusChanged -= Game_GameStatusChanged;
            game = new TicTacToeNet.Game();
            game.GameStatusChanged += Game_GameStatusChanged;
            return Task.FromResult(true);
        }
    }
}
