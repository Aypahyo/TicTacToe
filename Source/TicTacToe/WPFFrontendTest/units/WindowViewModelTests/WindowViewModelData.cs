using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.WPFFrontendTest.units.WindowViewModelTests
{
    internal class ShouldRaiseWhenMapIsSetCase
    {
        public IEnumerable<Tuple<int, int, char>> changes;
        public int ExpectedRaiseCount;
        public override string ToString()
        {
            return $"{ExpectedRaiseCount} -> {changes.First()}";
        }
    }

    internal class WindowViewModelData
    {
        internal static ShouldRaiseWhenMapIsSetCase[] ShouldRaiseWhenMapIsSetData()
        {
            var cases = new List<ShouldRaiseWhenMapIsSetCase>();

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new ShouldRaiseWhenMapIsSetCase()
                    {
                        changes = new []{ new Tuple<int, int, char>(col, row, 'X') },
                        ExpectedRaiseCount = 1
                    }); ;

            return cases.ToArray();
        }
    }
}