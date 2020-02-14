#include <CoreNative/Include/GameJudge.h>
#include <regex>

namespace TicTacToe
{

	struct GameJudge::Impl
	{
		std::regex winner;

		Impl(){
			std::string pattern{ R"(
			(O{3})|
			(X{3})|
			(X.{3}X.{3}X)|
			(O.{3}O.{3}O)|
			(X.{4}X.{4}X)|
			(O.{4}O.{4}O)|
			(X.{2}X.{2}X)|
			(O.{2}O.{2}O)
			)" };
			pattern = std::regex_replace(pattern, std::regex{ R"(\s*)" }, "");
			winner = std::regex{ pattern };
		}

		bool GameIsWon(const char board[12])
		{
			std::smatch m;
			std::string s{board, 12};
			return std::regex_search(s, m, winner);
		}
	};

	bool GameJudge::GameIsWon(const char board[12])
	{
		return impl->GameIsWon(board);
	}

	GameJudge::GameJudge() : impl{ std::make_unique<Impl>() } {};
	GameJudge::~GameJudge() = default;
}