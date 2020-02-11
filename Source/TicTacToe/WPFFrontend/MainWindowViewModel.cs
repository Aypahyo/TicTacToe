using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TicTacToe.WPFFrontend.GameService;

namespace TicTacToe.WPFFrontend
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _s = string.Empty;
        private readonly IGameService _gameService;

        public WindowViewModel(IGameService gameService)
        {
            MapClick = new CSVCommand(MapClickImpl);
            ControlClick = new CSVCommand(ControlClickImpl);
            _gameService = gameService ?? new NoGameService();
            WeakEventManager<IGameService, StatusEventArgs>.AddHandler(_gameService, nameof(_gameService.GameStatus), GameService_GameStatus);
        }

        private void GameService_GameStatus(object sender, StatusEventArgs e)
        {
            Systemstate = e.SystemState;
            for (int row = 0; row < 3; ++row)
                for (int col = 0; col < 3; ++col)
                {
                    try
                    {
                        SetMap(col, row, e.MAP[row][col]);
                    }
                    catch { 
                        //intentionally empty
                    }
                }
        }

        private void ControlClickImpl(string[] args)
        {
            var symbols = new[] { "H", "C" };
            if (args?.Length != 2) return;
            if (!symbols.Contains(args[0])) return;
            if (!symbols.Contains(args[1])) return;
            var newGameTask = _gameService.TryNewGame(string.Join(',', args));
            if(newGameTask.Result)
                ClearMap();
        }

        private void MapClickImpl(string[] args)
        {
            if (args?.Length != 2) return;

            if (int.TryParse(args[0], out int col) && int.TryParse(args[1], out int row))
            {
                var s = _gameService.GetCurrentSymbol();
                SetMap(col, row, s.Result);
            }
        }

        private bool TryUpdateMap(int col, int row, char c)
        {
            var allowed = Enumerable.Range(0, 3);
            if (!allowed.Contains(col)) return false;
            if (!allowed.Contains(row)) return false;
            if (MAP[col][row] == c) return false;

            var moveTask = _gameService.TryMove(col, row, c);
            if(moveTask.Result)
                MAP[col][row] = c;

            return moveTask.Result;
        }

        public void ClearMap()
        {
            bool changed = false;
            foreach (var col in Enumerable.Range(0, 3))
                foreach (var row in Enumerable.Range(0, 3))
                    changed |= TryUpdateMap(col, row, ' ');
            if (changed)
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MAP)));
        }

        public void SetMap(int col, int row, char c)
        {
            if (TryUpdateMap(col, row, c))
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MAP)));
        }


        public char[][] MAP { get; } = {
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' },
            new char[] { ' ' , ' ' , ' ' } };

        public string Systemstate
        {
            get { return _s; }
            set
            {
                value ??= string.Empty;
                if (string.Equals(_s, value)) return;
                _s = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Systemstate)));
            }
        }

        public ICommand ControlClick { get; }
        public ICommand MapClick { get; }
    }
}
