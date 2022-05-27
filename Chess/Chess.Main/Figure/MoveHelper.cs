using Chess.Main.Common;
using Chess.Main.Map;

namespace Chess.Main.Figure
{
    public static class MoveHelper
    {
        public static List<Point> GetPawnAvailableMoves(PawnChessFigureBase pawn, ChessMap map)
        {
            var result = new List<Point>();

            Point checkPoint;

            if (pawn.Side == ChessSide.White)
                checkPoint = new Point(pawn.Point.X, pawn.Point.Y - 1);
            else
                checkPoint = new Point(pawn.Point.X, pawn.Point.Y + 1);

            if (CheckForIndexOrEnemy(checkPoint, pawn, map))
                result.Add(checkPoint);

            if (!pawn.IsMoved)
            {
                if (pawn.Side == ChessSide.White)
                    checkPoint = new Point(pawn.Point.X, pawn.Point.Y - 2);
                else
                    checkPoint = new Point(pawn.Point.X, pawn.Point.Y + 2);

                if (CheckForIndexOrEnemy(checkPoint, pawn, map))
                    result.Add(checkPoint);
            }

            return result;
        }

        private static bool CheckForIndexOrEnemy(Point destinationPoint, ChessFigureBase sourceFigure, ChessMap map)
        {
            var indexCheck = destinationPoint.X >= 0
                             && destinationPoint.Y >= 0
                             && destinationPoint.X < ChessConstants.ChessMapLenght
                             && destinationPoint.Y < ChessConstants.ChessMapLenght;

            if (indexCheck is false)
                return false;

            var enemyCheck = map.GetFigure(destinationPoint);
            if (enemyCheck == null)
                return true;

            return enemyCheck.Side != sourceFigure.Side;
        }
    }
}