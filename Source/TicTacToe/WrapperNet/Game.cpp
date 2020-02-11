#include "Game.h"

using namespace System::Runtime::InteropServices;

namespace TicTacToeNet
{
	void gameStateChangedHandler(void* context, TicTacToe::Game* game, TicTacToe::GameState state);
	
	struct GameImpl
	{
		TicTacToe::Game game{};
		int registrationId;
		IntPtr parent;

		void Raise(std::string json)
		{
			Game^ targetGame = safe_cast<Game^>(static_cast<GCHandle>(parent).Target);
			System::String^ managedJson = gcnew System::String(json.c_str());
			targetGame->RaiseGameStatusChanged(managedJson);
		}

		GameImpl(IntPtr parent)
		{
			this->parent = parent;
			registrationId = game.RegisterGameStateChangedHandler((void*)this, gameStateChangedHandler);
		}

		~GameImpl()
		{
			game.UnRegisterGameStateChangedHandler(registrationId);
		}
	};

	void gameStateChangedHandler(void* context, TicTacToe::Game* game, TicTacToe::GameState state)
	{
		GameImpl* pimpl = (GameImpl*)context;
		pimpl->Raise(state.ToJson());
	}

	Game::Game()
	{
		auto thisHandle = static_cast<IntPtr>(GCHandle::Alloc(this));
		pimpl = new GameImpl(thisHandle);
	}

	Game::~Game()
	{
		delete pimpl;
	}

	void Game::RaiseGameStatusChanged(System::String^ json)
	{
		GameStatusChanged(this, json);
	}

	wchar_t Game::CurrentPlayer()
	{
		return pimpl->game.CurrentPlayer();
	}

	bool Game::Move(int row, int column, wchar_t player)
	{
		return pimpl->game.Move(row, column, (char)player);
	}

	bool Game::Move(GameMove ^ move)
	{
		int row = move->Row;
		int column = move->Column;
		wchar_t player = move->Player;
		return pimpl->game.Move(row, column, player);
	}
}

