using Chess.Models;
using Chess2.Models;
using Chess2.Services;
using Chess2.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess2
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel = new();
        private CellViewModel? _selectedKnight;
        private BoardLoader _boardLoader = new();

        private readonly (int x, int y)[] KnightMoves =
        {
            (-2, -1),
            (-2, 1),
            (-1, -2),
            (-1, 2),
            (1, -2),
            (1, 2),
            (2, -1),
            (2, 1)
        };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel;

            CreateBoard();
        }
        
        /*Обработка кнопок*/
        private void SelectKnight(CellViewModel cell)
        {
            ClearMarkers();

            _selectedKnight = cell;

            cell.IsSelected = true;

            foreach (var move in KnightMoves)
            {
                int nx = cell.X + move.x;
                int ny = cell.Y + move.y;

                if (!InsideBoard(nx, ny))
                    continue;

                var target = GetCell(nx, ny);

                // Союзная фигура
                if (target.Piece != null &&
                    target.Piece.IsWhite)
                {
                    target.Marker = MarkerType.Friendly;

                    continue;
                }

                // Опасная клетка
                if (IsCellUnderAttack(nx, ny))
                {
                    target.Marker = MarkerType.Dangerous;

                    continue;
                }

                target.Marker = MarkerType.Valid;
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            CellViewModel cell =
                (CellViewModel)button.Tag;

            // Ход
            if (_selectedKnight != null &&
                cell.Marker == MarkerType.Valid)
            {
                MoveKnight(_selectedKnight, cell);

                return;
            }

            // Выбор коня
            if (cell.Piece is Knight knight &&
                knight.IsWhite)
            {
                SelectKnight(cell);
            }
        }


        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            ClearMarkers();

            CreateBoard();

            _boardLoader.LoadRandomBoard(_viewModel);
        }

        /*Операции с доской*/
        private void CreateBoard()
        {
            _viewModel.Cells.Clear();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bool isWhite = (x + y) % 2 == 0;

                    _viewModel.Cells.Add(new CellViewModel
                    {
                        X = x,
                        Y = y,
                        Background = isWhite
                            ? Brushes.Beige
                            : Brushes.SaddleBrown
                    });
                }
            }
        }

        private void MoveKnight(
            CellViewModel from,
            CellViewModel to)
        {
            to.Piece = from.Piece;

            to.PieceSymbol = from.PieceSymbol;

            from.Piece = null;

            from.PieceSymbol = "";

            ClearMarkers();

            _selectedKnight = null;
        }
        /*Иснтрументы*/
        private bool InsideBoard(int x, int y)
        {
            return x >= 0 &&
                   x < 8 &&
                   y >= 0 &&
                   y < 8;
        }
        private CellViewModel GetCell(int x, int y)
        {
            return _viewModel.Cells
                .First(c => c.X == x && c.Y == y);
        }
        private void ClearMarkers()
        {
            foreach (var cell in _viewModel.Cells)
            {
                cell.Marker = MarkerType.None;
                cell.IsSelected = false;
            }
        }
        private bool IsCellUnderAttack(int x, int y)
        {
            foreach (var cell in _viewModel.Cells)
            {
                if (cell.Piece == null)
                    continue;

                if (cell.Piece.IsWhite)
                    continue;

                // Конь
                if (cell.Piece is Knight)
                {
                    foreach (var move in KnightMoves)
                    {
                        int nx = cell.X + move.x;
                        int ny = cell.Y + move.y;

                        if (nx == x && ny == y)
                            return true;
                    }
                }

                // Пешка
                if (cell.Piece is Pawn)
                {
                    if (cell.X - 1 == x &&
                        cell.Y + 1 == y)
                        return true;

                    if (cell.X + 1 == x &&
                        cell.Y + 1 == y)
                        return true;
                }

                // Ладья
                if (cell.Piece is Rook)
                {
                    if (CheckLineAttack(cell.X, cell.Y,
                        x, y,
                        new[] {
                    (1,0),(-1,0),
                    (0,1),(0,-1)
                        }))
                        return true;
                }

                // Король
                if (cell.Piece is King)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            // Пропуск своей клетки
                            if (dx == 0 && dy == 0)
                                continue;

                            int nx = cell.X + dx;
                            int ny = cell.Y + dy;

                            if (nx == x && ny == y)
                                return true;
                        }
                    }
                }

                // Слон
                if (cell.Piece is Bishop)
                {
                    if (CheckLineAttack(cell.X, cell.Y,
                        x, y,
                        new[] {
                    (1,1),(1,-1),
                    (-1,1),(-1,-1)
                        }))
                        return true;
                }

                // Ферзь
                if (cell.Piece is Queen)
                {
                    if (CheckLineAttack(cell.X, cell.Y,
                        x, y,
                        new[] {
                    (1,0),(-1,0),
                    (0,1),(0,-1),
                    (1,1),(1,-1),
                    (-1,1),(-1,-1)
                        }))
                        return true;
                }
            }

            return false;
        }
        private bool CheckLineAttack(
            int startX,
            int startY,
            int targetX,
            int targetY,
            (int x, int y)[] directions)
        {
            foreach (var dir in directions)
            {
                int x = startX;
                int y = startY;

                while (true)
                {
                    x += dir.x;
                    y += dir.y;

                    if (!InsideBoard(x, y))
                        break;

                    var cell = GetCell(x, y);

                    if (x == targetX &&
                        y == targetY)
                        return true;

                    if (cell.Piece != null)
                        break;
                }
            }

            return false;
        }
    }
}