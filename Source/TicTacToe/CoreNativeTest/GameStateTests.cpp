#include <gtest/gtest.h>
#include <CoreNative/Include/TicTacToe.h>
#include <regex>
#include "Helper.hpp"

namespace CoreTests
{
    TEST(GameState, JSonEmpty) {
        TicTacToe::GameState state{};

        auto expected = clean(
            R"({
                "NextPlayer" : 'X',
                "Winner" : ' ',
                "Board" : "   ,   ,   ",
                "Moves" : [ ] 
                } )");
        auto actual = clean(state.ToJson());

        ASSERT_EQ(expected, actual);
    }


    TEST(GameState, JSonFilled) {
        TicTacToe::GameState state{};
        state.NextPlayer = '7';
        state.Winner = 'F';
        state.SetBoardState("123,456,DEG");
        state.AppendMove(TicTacToe::GameMove{ 1, 2, 'K' });

        auto expected = clean(
            R"({
                "NextPlayer" : '7',
                "Winner" : 'F',
                "Board" : "123,456,DEG",
                "Moves" : [
                               { "Row" : 1, "Column" : 2, "Player" : 'K' }
                          ] 
                } )");
        auto actual = clean(state.ToJson());

        ASSERT_EQ(expected, actual);
    }
}