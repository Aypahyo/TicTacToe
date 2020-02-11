#include <gtest/gtest.h>
#include <CoreNative/Include/TicTacToe.h>
#include <regex>
#include "Helper.hpp"

namespace CoreTests
{
    TEST(GameMove, JSonEmpty) {
        TicTacToe::GameMove move{};
        auto expected = clean(R"({ "Row" : -1, "Column" : -1, "Player" : ' ' } )");
        auto actual = clean(move.ToJson());
        ASSERT_EQ(expected, actual);
    }


    TEST(GameMove, JSonFilled) {
        TicTacToe::GameMove move{};
        move.row = 7;
        move.column = 5;
        move.player = 'M';
        auto expected = clean(R"({ "Row" : 7, "Column" : 5, "Player" : 'M' } )");
        auto actual = clean(move.ToJson());
        ASSERT_EQ(expected, actual);
    }
}