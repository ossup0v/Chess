using Chess.Main.Common;
using Chess.Main.Map;
using Chess.Main.Player;

namespace Chess.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Guid whitePlayerId = Guid.NewGuid();
            ChessGame game = new ChessGame();

            var whitePlayer = new HumanChessPlayer(ChessSide.White, whitePlayerId, game.ProcessMapInput);

            game.SetPlayer(new HumanChessPlayer(ChessSide.Black, Guid.NewGuid(), game.ProcessMapInput));
            game.SetPlayer(whitePlayer);

            whitePlayer.ProcessMapInput(new Point(1, 6));

            ShowMap(game.GetMap(ChessSide.White));

            whitePlayer.ProcessMapInput(new Point(1, 4));

            ShowMap(game.GetMap(ChessSide.White));
            
            Console.ReadLine();
        }

        private static void ShowMap(string map)
        {
            Console.Clear();
            Console.WriteLine(map);
        }
    }
    
    public class ChessGame
    {
        private ChessPlayerBase CurrentMovePlayer => _currentMove % 2 == 0 ? _whitePlayer : _blackPlayer;
        private ChessPlayerBase _whitePlayer;
        private ChessPlayerBase _blackPlayer;
        
        private ulong _currentMove = 0;

        private readonly ChessMap _map;

        public ChessGame()
        {
            _map = new ChessMap();
        }

        public void SetPlayer(ChessPlayerBase player)
        {
            switch (player.Side)
            {
                case ChessSide.Black:
                    _blackPlayer = player;
                    break;
                case ChessSide.White:
                    _whitePlayer = player;
                    break;
                default:
                    break;
            }
        }

        public InputProcessResult ProcessMapInput(Guid playerId, Point point)
        {
            var player = GetPlayer(playerId);

            if (player == null)
                return InputProcessResult.Fail($"Can't find player with id {playerId}");

            var playerFigure = player.PeekedFigure;

            if (playerFigure == null)
                return InputProcessResult.Create(TryPeekFigure(playerId, point));

            return InputProcessResult.Create(TryToMove(playerId, point));
        }

        private bool TryPeekFigure(Guid playerId, Point point)
        {
            var player = GetPlayer(playerId);

            var figure = _map.GetFigure(point);

            if (figure == null || figure.Side != player.Side)
                return false;
            
            if (player.IsFigurePeeked)
                player.RemoveFigure();
            else
                player.PeekFigure(figure);

            return true;
        }

        private bool TryToMove(Guid playerId, Point destinationPoint)
        {
            if (CurrentMovePlayer.PlayerId != playerId)
                return false;

            if (CurrentMovePlayer.IsFigurePeeked is false)
                return false;

            if (CurrentMovePlayer.PeekedFigure != null &&
                CurrentMovePlayer.PeekedFigure.GetPossibleMoves(_map).Contains(destinationPoint) is false)
                return false;

            if (_map.TryMoveFigure(CurrentMovePlayer?.PeekedFigure?.Point, destinationPoint))
            {
                IncreaseMove();
                return  true;
            }
            return  false;
        }

        private ChessPlayerBase GetPlayer(Guid playerId)
        {
            if (playerId == _whitePlayer.PlayerId)
                return _whitePlayer;
            if (playerId == _blackPlayer.PlayerId)
                return _blackPlayer;

            throw new Exception($"Can't find player with id {playerId}");
        }
        
        private ChessPlayerBase GetPlayer(ChessSide playerSide)
        {
            if (playerSide == _whitePlayer.Side)
                return _whitePlayer;
            if (playerSide== _blackPlayer.Side)
                return _blackPlayer;

            throw new Exception($"Can't find player with side {playerSide}");
        }

        private void IncreaseMove()
        {
            _currentMove++;
        }

        public string GetMap()
        {
            return _map.GetStringMap();
        }

        public string GetMap(ChessSide playerSide)
        {
            var map = _map.GetMap();
            var player = GetPlayer(playerSide);


            if (player.IsFigurePeeked)
            {
                var positionsToMove = player.PeekedFigure?.GetPossibleMoves(_map);
                
                if (positionsToMove == null)
                    return ChessMap.CastMap(map);
                
                foreach (var position in positionsToMove)
                    map[position.X, position.Y] = ChessConstants.ShowMoveSymbol;
            }

            return ChessMap.CastMap(map);
        }
    }
}