#include <CoreNative/Include/Game.h>
#include <string>
#include <regex>

namespace TicTacToe
{
	
	struct Game::Impl
	{
		std::regex winner{ "([XO]{3})|(X.{3}X.{3}X)|(O.{3}O.{3}O)|(X.{4}X.{4}X)|(O.{4}O.{4}O)|(X.{2}X.{2}X)|(O.{2}O.{2}O)" };
		GameState state{};
		bool Move(GameMove move)
		{
			if (StaticallyInvalid(move))
				return false;
			if (MoveNotPossible(move))
				return false;
			if (!state.AppendMove(move))
				return false;
			UpdateBoard(move);
			if (GameIsWon())
			{
				state.Winner = move.player;
				state.NextPlayer = ' ';
			}
			else
			{
				UpdatePlayer(move);
			}
			return true;
		}

		bool GameIsWon()
		{
			std::smatch m;
			std::string s{state.Board};
			return std::regex_search(s, m, winner);
		}

		bool StaticallyInvalid(GameMove move)
		{
			if (move.column < 0) return true;
			if (move.row < 0) return true;
			if (move.column > 2) return true;
			if (move.row > 2) return true;
			if (!(move.player == 'X' || move.player == 'O')) return true;
			return false;
		}

		bool MoveNotPossible(GameMove move)
		{
			int index = move.row * 4 + move.column;
			return state.Board[index] != ' ';
		}

		void UpdateBoard(GameMove move)
		{
			int index = move.row * 4 + move.column;
			state.Board[index] = move.player;
		}

		void UpdatePlayer(GameMove move)
		{
			state.NextPlayer = 'X';
			if (move.player == 'X')
				state.NextPlayer = 'O';
		}

		GameState GetState()
		{
			return state;
		}

		Impl() {};
		~Impl() {};
	};


	Game::Game():impl(std::make_unique<Game::Impl>())
	{
	}

	Game::~Game()
	{
	}

	char Game::CurrentPlayer()
	{
		return impl->state.NextPlayer;
	}

	bool Game::Move(int row, int column, char player)
	{
		return impl->Move(GameMove{ row, column, player });
	}

	bool Game::Move(GameMove move)
	{
		return impl->Move(move);
	}

	GameState Game::GetState()
	{
		return impl->GetState();
	}
}