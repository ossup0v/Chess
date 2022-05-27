using Chess.Main.Common;
using Chess.Main.Figure;
using System.Text;

namespace Chess.Main.Map
{
    public class ChessMap
    {
        private readonly ChessField[,] _map = new ChessField[ChessConstants.ChessMapLenght, ChessConstants.ChessMapLenght];
        private ChessField this[Point point]
        {
            get => _map[point.X, point.Y];
            set => _map[point.X, point.Y] = value;
        }

        public ChessMap()
        {

            for (int x = 0; x < ChessConstants.ChessMapLenght; x++)
                for (int y = 0; y < ChessConstants.ChessMapLenght; y++)
                    _map[y, x] = ChessField.Empty(new Point(x, y));

            #region Black

            Point point = new Point(0, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(1, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(2, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(3, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(4, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(5, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(6, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));
            point = new Point(7, 1); this[point] = new ChessField(point, new BlackPawnChessFigure(point));

            #endregion

            #region White

            point = new Point(0, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(1, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(2, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(3, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(4, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(5, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(6, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));
            point = new Point(7, 6); this[point] = new ChessField(point, new WhitePawnChessFigure(point));

            #endregion
        }

        public ChessFigureBase? GetFigure(Point point) => this[point].CurrentFigure;
        public bool IsHaveFigure(Point point) => this[point].IsEmpty is false;

        public bool TryMoveFigure(Point? from, Point? to)
        {
            if (from == null)
                return false;

            if (to == null)
                return false;

            var figureToMove = GetFigure(from.Value);

            if (figureToMove == null)
                return false;

            if (IsHaveFigure(to.Value) && GetFigure(to.Value)?.Side == figureToMove.Side)
                return false;

            MoveTo(from.Value, to.Value);

            return true;
        }

        private void MoveTo(Point from, Point to)
        {
            this[to].SetFigure(this[from].CurrentFigure);
            this[from].RemoveFigure();
        }

        public string GetStringMap()
        {
            var sb = new StringBuilder(8 * 8);

            for (int x = 0; x < ChessConstants.ChessMapLenght; x++)
            {
                for (int y = 0; y < ChessConstants.ChessMapLenght; y++)
                    sb.Append(_map[y, x].Symbol);

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string?[,] GetMap()
        {
            string?[,] result = new string?[ChessConstants.ChessMapLenght, ChessConstants.ChessMapLenght];

            for (int x = 0; x < ChessConstants.ChessMapLenght; x++)
                for (int y = 0; y < ChessConstants.ChessMapLenght; y++)
                    result[y, x] = _map[y, x].Symbol;

            return result;
        }

        public static string CastMap(string[,] source)
        {
            var sb = new StringBuilder(8 * 8);

            for (int x = 0; x < ChessConstants.ChessMapLenght; x++)
            {
                for (int y = 0; y < ChessConstants.ChessMapLenght; y++)
                    sb.Append(source[y, x]);

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}