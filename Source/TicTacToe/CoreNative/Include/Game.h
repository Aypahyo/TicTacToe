#pragma once
#include <memory>
#include <CoreNative/Include/GameState.h>

namespace TicTacToe
{
	class Game;

	typedef void (*GameStateChangedHandler)(void* context, Game* sender, GameState state);

	class Game
	{
	private:
		struct Impl;
		std::unique_ptr<Impl> impl;
	public:
		Game();
		~Game();
		int RegisterGameStateChangedHandler(void* context, GameStateChangedHandler handler);
		void UnRegisterGameStateChangedHandler(int registrationID);
		char CurrentPlayer();
		bool Move(int row, int column, char player);
		bool Move(GameMove move);
		GameState GetState();
	};

}