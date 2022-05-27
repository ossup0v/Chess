using Chess.Main.Common;
using Chess.Main.Map;

namespace Chess.Main.Figure
{
    public sealed class WhitePawnChessFigure : PawnChessFigureBase
    {
        public WhitePawnChessFigure(Point point)
            : base(ChessConstants.WhitePawnSymbol, ChessSide.White, point, Guid.NewGuid()) { }

        public override List<Point> GetPossibleMoves(ChessMap map)
        {
            return MoveHelper.GetPawnAvailableMoves(this, map);
        }
    }
}