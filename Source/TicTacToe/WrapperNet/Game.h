#pragma once
#include <CoreNative/Include/TicTacToe.h>
#include <WrapperNet/GameMove.h>
using namespace System;

namespace TicTacToeNet {

	struct GameImpl;

	public ref class Game
	{
	private:
		GameImpl* pimpl;
	internal:
		void RaiseGameStatusChanged(System::String^ json);
	public:
		Game();
		~Game();
		event EventHandler<System::String^>^ GameStatusChanged;
		wchar_t CurrentPlayer();
		bool Move(int row, int column, wchar_t player);
		bool Move(GameMove ^ move);
	};
}
