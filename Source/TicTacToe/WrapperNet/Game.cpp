#include "Game.h"

namespace TicTacToeNet
{
	Game::Game()
	{
		game = new TicTacToe::Game();
	}
	Game::~Game()
	{
		delete game;
	}
	char Game::CurrentPlayer()
	{
		return game->CurrentPlayer();
	}
	bool Game::Move(int row, int column, wchar_t player)
	{
		return game->Move(row, column, (char)player);
	}
	bool Game::Move(GameMove move)
	{
		return game->Move(move.Row, move.Column, (char)move.Player);
	}


}

