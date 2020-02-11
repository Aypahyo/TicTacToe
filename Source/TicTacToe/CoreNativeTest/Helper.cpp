#pragma once
#include "Helper.hpp"
#include <regex>

std::string clean(std::string unclean)
{
    auto spaces = std::regex_replace(unclean, std::regex{ R"(\s+)" }, " ");
    auto comtrols = std::regex_replace(spaces, std::regex{ R"(\s*([:{},\[\]])\s*)" }, "$1");
    auto trimmed = std::regex_replace(comtrols, std::regex{ R"((^\s+)|(\s+$))" }, "");
    return trimmed;
}
