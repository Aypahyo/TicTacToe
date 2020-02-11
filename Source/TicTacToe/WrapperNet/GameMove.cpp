#include "GameMove.h"
#include <string>
#include <sstream>

namespace TicTacToeNet
{
	struct GameMoveImpl {
		TicTacToe::GameMove gameMove{};
	};
	
	GameMove::GameMove(int row, int column, wchar_t player) {
		pimpl = new GameMoveImpl();
		pimpl->gameMove.row = row;
		pimpl->gameMove.column = column;
		pimpl->gameMove.player = player;
	}

	GameMove::~GameMove()
	{
		delete pimpl;
	}

	int GameMove::getRow() { return pimpl->gameMove.row; }
	void GameMove::setRow(int value) { pimpl->gameMove.row = value; }
	int GameMove::getColumn() { return pimpl->gameMove.column; }
	void GameMove::setColumn(int value) { pimpl->gameMove.column = value; }
	wchar_t GameMove::getPlayer() { return pimpl->gameMove.player; }
	void GameMove::setPlayer(wchar_t value) { pimpl->gameMove.player = value; }


	String^ GameMove::ToString()
	{
		return gcnew System::String(pimpl->gameMove.ToJson().c_str());
	}
}
