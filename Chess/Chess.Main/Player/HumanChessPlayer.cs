using Chess.Main.Common;

namespace Chess.Main.Player
{
    public class HumanChessPlayer : ChessPlayerBase
    {
        public HumanChessPlayer(ChessSide side, Guid playerId, Func<Guid, Point, InputProcessResult> processMapInput) 
            : base(side, playerId, processMapInput) { }
    }
}