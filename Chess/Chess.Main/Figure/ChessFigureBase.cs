using Chess.Main.Common;
using Chess.Main.Map;

namespace Chess.Main.Figure
{
    public abstract class ChessFigureBase
    {
        public readonly string Symbol;
        public readonly ChessSide Side;
        public readonly Guid FigureId;
        public Point Point { get; private set; }

        public ChessFigureBase(string symbol, ChessSide side, Point point, Guid figureId)
        {
            Side = side;
            Symbol = symbol;
            Point = point;
            FigureId = figureId;
        }

        public virtual void MoveTo(Point newPoint)
        {
            Point = newPoint;
        }

        public abstract List<Point> GetPossibleMoves(ChessMap map);
    }
}