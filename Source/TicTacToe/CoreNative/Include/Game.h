#pragma once
#include <memory>
#include <CoreNative/Include/GameState.h>

namespace TicTacToe
{
	class Game
	{
	private:
		struct Impl;
		std::unique_ptr<Impl> impl;
	public:
		Game();
		~Game();
		char CurrentPlayer();
		bool Move(int row, int column, char player);
		bool Move(GameMove move);
		GameState GetState();
	};

}