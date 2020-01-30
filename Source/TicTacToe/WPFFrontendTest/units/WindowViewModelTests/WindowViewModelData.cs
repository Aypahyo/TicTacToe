using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.WPFFrontendTest.units.WindowViewModelTests
{
    public class MapSetCase
    {
        public IEnumerable<Tuple<int, int, char>> changes;
        public int ExpectedRaiseCount;
        public bool MapChanged;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .Append($"events {ExpectedRaiseCount} changed ")
                .Append($"{MapChanged} from");
            foreach (var change in changes)
                sb.Append($" {change}");
            return sb.ToString();
        }
    }

    public class MapClickCase
    {
        public IEnumerable<string> args;
        public int ExpectedRaiseCount;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"events {ExpectedRaiseCount} from ");
            foreach (var click in args)
                sb.Append($" {click ?? "<null>"}");
            return sb.ToString();
        }
    }

    public class WindowViewModelData
    {
        internal static MapClickCase[] MapClickData()
        {
            var cases = new List<MapClickCase>();

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new MapClickCase() { 
                        args = new[] { $"{col},{row}" }, 
                        ExpectedRaiseCount = 1 });
            {
                var args = new List<string>();
                foreach (var col in Enumerable.Range(0, 3))
                    foreach (var row in Enumerable.Range(0, 3))
                        args.Add($"{col},{row}");

                cases.Add(new MapClickCase(){ 
                    args = args, 
                    ExpectedRaiseCount = 9 });
            }

            cases.Add(new MapClickCase() { 
                args = new[] { $"foooo" }, 
                ExpectedRaiseCount = 0 });

            cases.Add(new MapClickCase()
            {
                args = new[] { $"1,-2" },
                ExpectedRaiseCount = 0
            });

            cases.Add(new MapClickCase()
            {
                args = new string[] { null },
                ExpectedRaiseCount = 0});

            return cases.ToArray();
        }

        internal static MapSetCase[] MapSetCaseData()
        {
            var cases = new List<MapSetCase>();

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new MapSetCase()
                    {
                        changes = new []{ new Tuple<int, int, char>(col, row, 'X') },
                        ExpectedRaiseCount = 1,
                        MapChanged = true
                    });

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new MapSetCase()
                    {
                        changes = new[] { new Tuple<int, int, char>(col, row, ' ') },
                        ExpectedRaiseCount = 0,
                        MapChanged = false
                    });

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new MapSetCase()
                    {
                        changes = new[] { 
                            new Tuple<int, int, char>(col, row, 'X'),
                            new Tuple<int, int, char>(col, row, ' ') },
                        ExpectedRaiseCount = 2,
                        MapChanged = false
                    });

            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    cases.Add(new MapSetCase()
                    {
                        changes = new[] {
                            new Tuple<int, int, char>(col, row, 'X'),
                            new Tuple<int, int, char>(col, row, 'X') },
                        ExpectedRaiseCount = 1,
                        MapChanged = true
                    });

            cases.Add(new MapSetCase()
            {
                changes = new[] { new Tuple<int, int, char>(-1, -1, 'X') },
                ExpectedRaiseCount = 0,
                MapChanged = false
            });

            cases.Add(new MapSetCase()
            {
                changes = new[] { new Tuple<int, int, char>(4, 4, 'X') },
                ExpectedRaiseCount = 0,
                MapChanged = false
            });

            cases.Add(new MapSetCase()
            {
                changes = new[] { new Tuple<int, int, char>(4, 1, 'X') },
                ExpectedRaiseCount = 0,
                MapChanged = false
            });

            cases.Add(new MapSetCase()
            {
                changes = new[] { new Tuple<int, int, char>(1, 4, 'X') },
                ExpectedRaiseCount = 0,
                MapChanged = false
            });

            return cases.ToArray();
        }
    }
}