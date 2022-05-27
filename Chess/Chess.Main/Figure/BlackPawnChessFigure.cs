using Chess.Main.Common;
using Chess.Main.Map;

namespace Chess.Main.Figure
{
    public sealed class BlackPawnChessFigure : PawnChessFigureBase
    {
        public BlackPawnChessFigure(Point point)
            : base(ChessConstants.BlackPawnSymbol, ChessSide.Black, point, Guid.NewGuid()) { }

        public override List<Point> GetPossibleMoves(ChessMap map)
        {
            return MoveHelper.GetPawnAvailableMoves(this, map);
        }
    }
}