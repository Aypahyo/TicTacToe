#pragma once
#include <memory>
namespace TicTacToe
{
	class GameJudge
	{
	private:
		struct Impl;
		std::unique_ptr<Impl> impl;
	public:
		GameJudge();
		~GameJudge();
		bool GameIsWon(const char board[12]);
	};
}