#pragma once
#include <CoreNative/Include/TicTacToe.h>
#include <WrapperNet/GameMove.h>
using namespace System;

namespace TicTacToeNet {
	public ref class Game
	{
	private:
		TicTacToe::Game * game;
	public:
		Game();
		~Game();

		char CurrentPlayer();
		bool Move(int row, int column, wchar_t player);
		bool Move(GameMove move);
		//GameState GetState();

	};
}
