#pragma once
#include <memory>
#include <CoreNative/Include/TicTacToe.h>
using namespace System;

namespace TicTacToeNet {
	struct GameMoveImpl;
	public ref class GameMove : Object
	{
	private:
		GameMoveImpl* pimpl;
		int getRow();
		void setRow(int);
		int getColumn();
		void setColumn(int);
		wchar_t getPlayer();
		void setPlayer(wchar_t);
	public:
		GameMove(int row, int column, wchar_t player);
		~GameMove();
		property int Row { 
			int get() { return getRow(); } 
			void set(int value) { setRow(value); }
		}
		property int Column {
			int get() { return getColumn(); }
			void set(int value) { setColumn(value); }
		}
		property wchar_t Player {
			wchar_t get() { return getPlayer(); }
			void set(wchar_t value) { setPlayer(value); }
		}

		virtual String^ ToString() override;
		 

	};
}