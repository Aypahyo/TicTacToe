#include "GameMove.h"
#include <string>
#include <sstream>

namespace TicTacToeNet
{
	GameMove::GameMove(int row, int column, wchar_t player) {
		gameMove = new TicTacToe::GameMove();
		gameMove->row = row;
		gameMove->column = column;
		gameMove->player = player;
	}

	GameMove::~GameMove()
	{
		delete gameMove;
	}


	String^ GameMove::ToString()
	{
		return gcnew System::String(gameMove->ToJson().c_str());
	}
}
