#pragma once
#include <CoreNative/Include/GameMove.h>

namespace TicTacToe
{
	class GameState
	{
	public:
		GameState();
		//Next Player to make a move or space for no player
		//example 'X', 'O' or ' '
		char NextPlayer = 'X';
		//' ' for an ongoing game, if game has been won 'X' or 'O'
		//example 'X', 'O' or ' '
		char Winner = ' '; 
		//zero terminated board state, comma seperated rows
		//last element always '\0'
		char Board[12] = "   ,   ,   ";
		//copies board state and ensures zero termination
		void SetBoardState(char board[12]);
		//GameMove.Zero terminated move history
		//last element always null
		GameMove Moves[10];
		//appends move and ensures zero termination
		//returns false if move was not appended
		bool AppendMove(GameMove move);

		bool operator ==(const GameState&) const;

	};
}