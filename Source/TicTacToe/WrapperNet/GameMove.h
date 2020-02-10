#pragma once
#pragma once
#include <CoreNative/Include/TicTacToe.h>
using namespace System;


namespace TicTacToeNet {
	public ref class GameMove : Object
	{
	private:
		//TODO pimpl
		TicTacToe::GameMove* gameMove;
	public:
		GameMove(int row, int column, wchar_t player);
		~GameMove();
		property int Row { 
			int get() { return gameMove->row; } 
			void set(int value) { gameMove->row = value; }
		}
		property int Column {
			int get() { return gameMove->column; }
			void set(int value) { gameMove->column = value; }
		}
		property wchar_t Player {
			wchar_t get() { return gameMove->player; }
			void set(wchar_t value) { gameMove->player = value; }
		}

		virtual String^ ToString() override;
		 

	};
}