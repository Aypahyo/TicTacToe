#include <gtest/gtest.h>
#include <CoreNative/Include/TicTacToe.h>

namespace CoreTests
{
    TEST(Game, CanCreate) {
        TicTacToe::Game game{};
    }

    TEST(Game, FirstPlayerX)
    {
        TicTacToe::Game game{};
        EXPECT_EQ('X', game.CurrentPlayer());
    }

    TEST(Game, AcceptMove)
    {
        TicTacToe::Game game{};
        bool success = game.Move(0, 0, 'X');
        EXPECT_TRUE(success);
    }

    TEST(Game, AcceptTieMoves)
    {
        TicTacToe::Game game{};
        bool success{ true };
        success &= game.Move(0, 0, 'X');
        success &= game.Move(0, 2, 'O');
        success &= game.Move(0, 1, 'X');
        success &= game.Move(1, 0, 'O');
        success &= game.Move(1, 1, 'X');
        success &= game.Move(2, 1, 'O');
        success &= game.Move(1, 2, 'X');
        success &= game.Move(2, 2, 'O');
        success &= game.Move(2, 0, 'X');
        EXPECT_TRUE(success);
    }

}