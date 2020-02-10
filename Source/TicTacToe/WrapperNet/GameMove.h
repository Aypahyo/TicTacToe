#pragma once
#pragma once
#include <CoreNative/Include/TicTacToe.h>
using namespace System;


namespace TicTacToeNet {
	public ref class GameMove
	{
	public:
		int Row;
		int Column;
		wchar_t Player;
	};
}