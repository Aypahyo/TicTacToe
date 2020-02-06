#pragma once
namespace TicTacToe
{
	struct GameMove
	{
		
		int row{-1}; 
		int column{-1}; 
		char player{' '};

		bool operator ==(const GameMove& other) const
		{
			if (player != other.player) return false;
			if (row != other.row) return false;
			if (column != other.column) return false;
			return true;
		}

		bool operator !=(const GameMove& other) const
		{
			return !(this->operator==(other));
		}

	};

	constexpr static GameMove GameMoveZero{};

}