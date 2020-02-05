#pragma once

namespace TicTacToe
{
	class Game
	{
	public:
		char CurrentPlayer();
		bool Move(int row, int column, char player);
	};

}