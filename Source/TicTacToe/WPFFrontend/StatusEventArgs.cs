using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.WPFFrontend
{
    public class StatusEventArgs : EventArgs
    {
        public string SystemState;
        public char[][] MAP;
    }
}
