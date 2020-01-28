using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TicTacToe.WPFFrontend
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _s = string.Empty;

        private bool TryUpdateMap(int col, int row, char c)
        {
            if (MAP[col][row] == c)
                return false;
            MAP[col][row] = c;
            return true;
        }

        public void ClearMap()
        {
            bool changed = false;
            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    changed |= TryUpdateMap(col, row, ' ');
        }

        public void SetMap(int col, int row, char c)
        {
            if (TryUpdateMap(col, row, c))
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MAP)));
        }


        public char[][] MAP = { 
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' } };




        public string Systemstate
        {
            get { return _s; }
            set
            {
                if (string.Equals(_s, value)) return;
                _s = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Systemstate)));
            }
        }
    }
}
