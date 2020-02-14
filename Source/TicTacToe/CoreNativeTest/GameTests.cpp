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

    TEST(Game, Winner_None)
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
        auto actual = game.GetState();
        EXPECT_EQ(' ', actual.Winner);
    }

    TEST(Game, Winner_NoneFontEndCase)
    {
        TicTacToe::Game game{};
        bool success{ true };
        success &= game.Move(0, 0, 'X');
        success &= game.Move(2, 0, 'O');
        success &= game.Move(1, 0, 'X');
        success &= game.Move(0, 1, 'O');
        success &= game.Move(1, 1, 'X');
        success &= game.Move(1, 2, 'O');
        success &= game.Move(2, 1, 'X');
        success &= game.Move(2, 2, 'O');
        success &= game.Move(0, 2, 'X');
        EXPECT_TRUE(success);
        auto actual = game.GetState();
        EXPECT_EQ(' ', actual.Winner);
    }

    TEST(Game, GetGameState)
    {
        TicTacToe::GameMove move1{ 0, 0, 'X' };
        TicTacToe::GameMove move2{ 0, 2, 'O' };
        TicTacToe::GameState expected{};
        expected.SetBoardState("X O,   ,   ");
        expected.NextPlayer = 'X';
        expected.AppendMove(move1);
        expected.AppendMove(move2);

        TicTacToe::Game game{};
        game.Move(move1);
        game.Move(move2);
        auto actual = game.GetState();

        EXPECT_EQ(expected, actual);
    }

    TEST(Game, Winner_X)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 0, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_DiagonaDown)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_DiagonaUp)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 2, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 0, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }
    TEST(Game, Winner_Row0)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 0, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'O' });
        game.Move(TicTacToe::GameMove{ 0, 2, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_Row1)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 1, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 2, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_Row2)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 2, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_Column0)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 0, 'X' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 0, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_Column1)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'X' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 1, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, Winner_Column2)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 2, 'X' });
        game.Move(TicTacToe::GameMove{ 0, 0, 'O' });
        game.Move(TicTacToe::GameMove{ 1, 2, 'X' });
        game.Move(TicTacToe::GameMove{ 1, 1, 'O' });
        game.Move(TicTacToe::GameMove{ 2, 2, 'X' });
        auto actual = game.GetState();
        EXPECT_EQ('X', actual.Winner);
    }

    TEST(Game, DenyIllegalMoves)
    {
        TicTacToe::Game game{};
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' }); //legal
        EXPECT_FALSE(game.Move(0, 0, 'O')) << "position was taken";
        EXPECT_FALSE(game.Move(-1, -1, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(1, -1, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(-1, 1, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(3, 3, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(1, 3, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(3, 1, 'O')) << "out of bounds";
        EXPECT_FALSE(game.Move(1, 1, ' ')) << "' ' not a player";
        EXPECT_FALSE(game.Move(1, 1, 'B')) << "'B' not a player";
    }

    TEST(Game, RegisterOne)
    {
        TicTacToe::Game game{};
        int context = 0;
        game.RegisterGameStateChangedHandler(&context, [](void* c, TicTacToe::Game* g, TicTacToe::GameState s){++(*(int*)c);});
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        ASSERT_EQ(1, context);
    }

    TEST(Game, RegisterTwo)
    {
        TicTacToe::Game game{};
        int one = 0;
        int two = 0;
        auto handler = [](void* c, TicTacToe::Game* g, TicTacToe::GameState s) {++(*(int*)c); };
        game.RegisterGameStateChangedHandler(&one, handler);
        game.RegisterGameStateChangedHandler(&two, handler);
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        ASSERT_EQ(1, one);
        ASSERT_EQ(1, two);
    }

    TEST(Game, UnRegister)
    {
        TicTacToe::Game game{};
        int one = 0;
        int two = 0;
        auto handler = [](void* c, TicTacToe::Game* g, TicTacToe::GameState s) {++(*(int*)c); };
        int id = game.RegisterGameStateChangedHandler(&one, handler);
        game.RegisterGameStateChangedHandler(&two, handler);
        game.UnRegisterGameStateChangedHandler(id);
        game.Move(TicTacToe::GameMove{ 0, 0, 'X' });
        ASSERT_EQ(0, one) << "one has been unregisterd";
        ASSERT_EQ(1, two) << "two should be active";
    }

}