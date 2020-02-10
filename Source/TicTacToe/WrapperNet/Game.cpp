#include "Game.h"

using namespace System::Runtime::InteropServices;

namespace TicTacToeNet
{
	void gameStateChangedHandler(void* context, TicTacToe::Game* game, TicTacToe::GameState state);
	void gameStateChangedHandler(void* context, TicTacToe::Game * game, TicTacToe::GameState state)
	{
		IntPtr thisHandle = (IntPtr)context;
		Game^ targetGame = safe_cast<Game^>(static_cast<GCHandle>(thisHandle).Target);

		System::String^ managedJson = gcnew System::String(state.ToJson().c_str());
		targetGame->Raise(managedJson);
	}

	Game::Game()
	{
		game = new TicTacToe::Game();
		thisHandle = static_cast<IntPtr>(GCHandle::Alloc(this));
		registrationId = game->RegisterGameStateChangedHandler((void*)thisHandle, gameStateChangedHandler);
	}
	Game::~Game()
	{
		game->UnRegisterGameStateChangedHandler(registrationId);
		static_cast<GCHandle>(thisHandle).Free();
		delete game;
	}

	void Game::Raise(System::String^ gameState)
	{
		this->GameStatusChanged(this, gameState);
	}

	char Game::CurrentPlayer()
	{
		return game->CurrentPlayer();
	}
	bool Game::Move(int row, int column, wchar_t player)
	{
		return game->Move(row, column, (char)player);
	}
	bool Game::Move(GameMove ^ move)
	{
		int row = move->Row;
		int column = move->Column;
		wchar_t player = move->Player;
		return game->Move(row, column, player);
	}
}

