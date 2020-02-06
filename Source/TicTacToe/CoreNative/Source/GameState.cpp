#include <CoreNative/Include/GameState.h>

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
