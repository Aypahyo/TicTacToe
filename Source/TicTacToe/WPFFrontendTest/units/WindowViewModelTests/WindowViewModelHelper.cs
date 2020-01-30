using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.WPFFrontend;

namespace TicTacToe.WPFFrontendTest.units.WindowViewModelTests
{
    class WindowViewModelHelper
    {
        internal static void ImportMap(WindowViewModel uut, string startmap)
        {
            var rows = startmap.Split(',');
            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    uut.SetMap(col, row, rows[row][col]);
        }

        internal static string ExportMap(WindowViewModel uut)
        {
            return string.Join(',', uut.MAP.Select(row => new String(row)));
        }
    }
}
