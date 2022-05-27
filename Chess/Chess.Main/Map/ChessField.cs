using Chess.Main.Common;
using Chess.Main.Figure;

namespace Chess.Main.Map
{
    public class ChessField
    {
        public readonly Point Point;
        public ChessFigureBase? CurrentFigure { get; private set; }
        public bool IsEmpty => CurrentFigure is null;

        public string Symbol => IsEmpty ? ChessConstants.EmptyFieldSymbol : CurrentFigure.Symbol;

        public static ChessField Empty(Point point) => new ChessField(point);
        public ChessField(Point point, ChessFigureBase? figure = null)
        {
            Point = point;
            CurrentFigure = figure;
        }

        public void SetFigure(ChessFigureBase figure)
        {
            CurrentFigure = figure;
            figure.MoveTo(Point);
        }

        public void RemoveFigure()
        {
            CurrentFigure = null;
        }
    }
}