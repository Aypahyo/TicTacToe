using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TicTacToe.WPFFrontend
{
    public class CSVCommand : ICommand
    {
        private Action<string[]> _executer;

        public CSVCommand(Action<string[]> executer)
        {
            _executer = executer;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter) => parameter is String;

        public void Execute(object parameter)
        {
            if(parameter is String par) _executer(
                par
                .Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToArray());
        }

    }
}
