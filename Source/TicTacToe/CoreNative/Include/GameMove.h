#pragma once
#include <sstream>

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

		std::string ToJson()
		{
			std::stringstream ss;
			ss << R"({ "Row": )" << row << R"(, "Column": )" << column << R"(, "Player": ')" << (char)player << R"(' })";
			return ss.str();
		}


	};

	constexpr static GameMove GameMoveZero{};

}