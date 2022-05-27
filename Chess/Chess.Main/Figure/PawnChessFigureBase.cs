using Chess.Main.Common;

namespace Chess.Main.Figure
{
    public abstract class PawnChessFigureBase : ChessFigureBase
    {
        protected PawnChessFigureBase(string symbol, ChessSide side, Point point, Guid figureId)
            : base(symbol, side, point, figureId) { }

        public bool IsMoved { get; private set; } = false;

        public override void MoveTo(Point newPoint)
        {
            IsMoved = true;
            base.MoveTo(newPoint);
        }
    }
}