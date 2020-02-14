#include <gtest/gtest.h>
#include <CoreNative/Include/TicTacToe.h>

namespace CoreTests
{
	TicTacToe::GameJudge judge{};
	TEST(GameJudge, EmptyNotWon)
	{
		ASSERT_FALSE(judge.GameIsWon("   ,   ,   "));
	}

	TEST(GameJudge, WonByX)
	{
		ASSERT_TRUE(judge.GameIsWon("XXX,   ,   "));
		ASSERT_TRUE(judge.GameIsWon("   ,XXX,   "));
		ASSERT_TRUE(judge.GameIsWon("   ,   ,XXX"));
		ASSERT_TRUE(judge.GameIsWon("X  ,X  ,X  "));
		ASSERT_TRUE(judge.GameIsWon(" X , X , X "));
		ASSERT_TRUE(judge.GameIsWon("  X,  X,  X"));
		ASSERT_TRUE(judge.GameIsWon("X  , X ,  X"));
		ASSERT_TRUE(judge.GameIsWon("  X, X ,X  "));
	}

	TEST(GameJudge, WonByO)
	{
		ASSERT_TRUE(judge.GameIsWon("OOO,   ,   "));
		ASSERT_TRUE(judge.GameIsWon("   ,OOO,   "));
		ASSERT_TRUE(judge.GameIsWon("   ,   ,OOO"));
		ASSERT_TRUE(judge.GameIsWon("O  ,O  ,O  "));
		ASSERT_TRUE(judge.GameIsWon(" O , O , O "));
		ASSERT_TRUE(judge.GameIsWon("  O,  O,  O"));
		ASSERT_TRUE(judge.GameIsWon("O  , O ,  O"));
		ASSERT_TRUE(judge.GameIsWon("  O, O ,O  "));
	}

	TEST(GameJudge, NotWon)
	{
		ASSERT_FALSE(judge.GameIsWon("OXO,   ,   "));
		ASSERT_FALSE(judge.GameIsWon("   ,XOO,   "));
		ASSERT_FALSE(judge.GameIsWon("   ,   ,OXO"));
		ASSERT_FALSE(judge.GameIsWon("O  ,O  ,X  "));
		ASSERT_FALSE(judge.GameIsWon(" O , O , X "));
		ASSERT_FALSE(judge.GameIsWon("  O,  O,  X"));
		ASSERT_FALSE(judge.GameIsWon("O  , O ,  X"));
		ASSERT_FALSE(judge.GameIsWon("  O, O ,X  "));
	}

	TEST(GameJudge, WonNormalGames)
	{
		ASSERT_TRUE(judge.GameIsWon("X  ,XO ,X O"));
		ASSERT_TRUE(judge.GameIsWon("XO ,XO ,X  "));
		ASSERT_TRUE(judge.GameIsWon("OX ,OX , X "));
		ASSERT_TRUE(judge.GameIsWon(" OX, OX,  X"));
		ASSERT_TRUE(judge.GameIsWon("XXX,OO ,   "));
		ASSERT_TRUE(judge.GameIsWon("OO ,XXX,   "));
		ASSERT_TRUE(judge.GameIsWon("OO ,   ,XXX"));
		ASSERT_TRUE(judge.GameIsWon("XO , XO,  X"));
		ASSERT_TRUE(judge.GameIsWon(" OX, X ,XO  "));
	}
}