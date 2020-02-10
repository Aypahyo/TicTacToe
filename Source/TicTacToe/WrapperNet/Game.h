#pragma once
#include <CoreNative/Include/TicTacToe.h>
#include <WrapperNet/GameMove.h>
using namespace System;

namespace TicTacToeNet {
	public ref class Game
	{
	private:
		//TODO refactor with pimpl idiom
		TicTacToe::Game * game;
		IntPtr thisHandle;
		int registrationId;
	public:
		Game();
		~Game();
		event EventHandler<System::String^>^ GameStatusChanged;
		void Raise(System::String^);
		char CurrentPlayer();
		bool Move(int row, int column, wchar_t player);
		bool Move(GameMove ^ move);
	};
}
