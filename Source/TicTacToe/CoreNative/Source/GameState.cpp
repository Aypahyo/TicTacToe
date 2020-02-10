#include <CoreNative/Include/GameState.h>
#include <sstream>

TicTacToe::GameState::GameState()
	:Moves{}
{
	for (int i = 0; i < 10; i++)
		Moves[i] = GameMoveZero;
}

void TicTacToe::GameState::SetBoardState(char board[12])
{
	for (int i = 0; i < 12; i++)
		Board[i] = board[i];
	Board[11] = 0;
}

bool TicTacToe::GameState::AppendMove(GameMove move)
{
	for (int i = 0; i < 9; i++)
		if (Moves[i] == GameMoveZero)
		{
			Moves[i] = move;
			return true;
		}
	return false;
}

std::string TicTacToe::GameState::ToJson()
{
	std::stringstream ss;
	ss << R"(
	{
			"NextPlayer" : ')" << NextPlayer << R"(',
			"Winner" : ')" << (char)Winner << R"(',
			"Board" : ")" << Board << R"(",
			"Moves" : 
			[)";
	bool more{ false };
	for (int i = 0; i < 9; i++)
	{
		if (Moves[i] == GameMoveZero) break;
		if (more) ss << ",\n";
		ss << Moves[i].ToJson();
		more = true;
	}
	ss << R"( ] } )";
	return ss.str();
}

bool TicTacToe::GameState::operator==(const GameState& other) const
{
	bool areEqual = true;
	if (this->NextPlayer != other.NextPlayer) return false;
	for (int i = 0; i < 12; i++)
		if(this->Board[i] != other.Board[i]) return false;
	for (int i = 0; i < 10; i++)
		if(this->Moves[i] != other.Moves[i]) return false;
	return true;
}
