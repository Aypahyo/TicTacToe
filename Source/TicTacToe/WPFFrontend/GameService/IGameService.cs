using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.WPFFrontend.GameService
{

    public interface IGameService
    {
        /// <summary>
        /// when the game state changes a notification is generated
        /// </summary>
        event EventHandler<StatusEventArgs> GameStatus;

        /// <summary>
        /// should alternate two symbols for the most part.
        /// gives a valid symbol for tryMove if the game expects a move
        /// </summary>
        /// <returns>
        /// if a game is on progress the symbol of the player expected to make a move
        /// if not in progress the return is undefined
        /// </returns>
        Task<char> GetCurrentSymbol();

        /// <summary>
        /// tries to set a move on the 3 by 3 game board
        /// </summary>
        /// <param name="col">
        /// column on the board: 0, 1, 2
        /// </param>
        /// <param name="row">
        /// row on the board: 0, 1, 2
        /// </param>
        /// <param name="symbol">
        /// has to be the next symbol 
        /// </param>
        /// <returns>
        /// whether or not the move happened
        /// </returns>
        Task<bool> TryMove(int col, int row, char symbol);

        /// <summary>
        /// starts a new game for this client
        /// </summary>
        /// <param name="gameType">
        /// "H,H" Human vs Human
        /// "H,C" Human vs Computer
        /// "C,H" Computer vs Human
        /// "C,C" Computer vs Computer
        /// </param>
        /// <returns>
        /// wheter or not the game start happened
        /// </returns>
        Task<bool> TryNewGame(string gameType);
    }
}
