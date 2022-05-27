using Chess.Main.Common;
using Chess.Main.Figure;

namespace Chess.Main.Player
{
    //human & bot & hard bot & exct
    public abstract class ChessPlayerBase
    {
        public ChessFigureBase? PeekedFigure { get; private set; }
        public bool IsFigurePeeked => PeekedFigure is not null;
        public Func<Guid, Point, InputProcessResult> MapInput;

        public readonly ChessSide Side;
        public readonly Guid PlayerId;

        public ChessPlayerBase(ChessSide side, Guid playerId, Func<Guid, Point, InputProcessResult> processMapInput)
        {
            Side = side;
            PlayerId = playerId;
            MapInput = processMapInput;
        }

        public void PeekFigure(ChessFigureBase figure)
        {
            PeekedFigure = figure;
        }

        public void RemoveFigure()
        {
            PeekedFigure = null;
        }

        public virtual InputProcessResult ProcessMapInput(Point point)
        { 
            return MapInput(PlayerId, point);
        }
    }
}