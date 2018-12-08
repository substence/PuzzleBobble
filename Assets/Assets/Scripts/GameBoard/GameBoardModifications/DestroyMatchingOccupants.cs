using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyMatchingOccupants : AbstractGameBoardModifier, IGameBoardModifier
{
    [SerializeField]
    private int amountOfMatchesForDestruction = 3;

    void Start()
    {
        gameBoard.AddedOccupantAction += GameBoard_AddedOccupantAction;
    }

    private void GameBoard_AddedOccupantAction(IGridOccupant obj)
    {
        List<IGridOccupant> matchingOccupants = GetMatchesAroundForOccupant(obj, gameBoard);
        if (matchingOccupants.Count < amountOfMatchesForDestruction)
        {
            return;
        }
        for (int i = 0; i < matchingOccupants.Count; i++)
        {
            IGridOccupant occupant = matchingOccupants[i];
            gameBoard.RemoveOccupantAt(occupant.x, occupant.y);
            Destroy(occupant.graphic);
        }
    }

    public static List<IGridOccupant> GetMatchesAroundForOccupant(IGridOccupant occupant, GameBoard gameBoard)
    {
        List<IGridOccupant> matches = new List<IGridOccupant>();
        IMatchableOccupant matchableOccupant = occupant as IMatchableOccupant;

        if (matchableOccupant == null)
        {
            return matches;
        }

        List<IGridOccupant> nodesToCheck = GetNeighboringOccupants(occupant, gameBoard);

        while(nodesToCheck.Count > 0)
        {
            IGridOccupant nodeToCheck = nodesToCheck[0];
            nodesToCheck.RemoveAt(0);
            if (nodeToCheck is IMatchableOccupant && (matchableOccupant as IMatchableOccupant).doesMatchWith(matchableOccupant))
            {
                List<IGridOccupant> matchingOccupantsNeighbors = GetNeighboringOccupants(nodeToCheck, gameBoard);
                nodesToCheck = mergeListsWithoutDuplicates(nodesToCheck, matchingOccupantsNeighbors);
            }
        }
        return matches;
    }

    private static List<IGridOccupant> mergeListsWithoutDuplicates(List<IGridOccupant> x, List<IGridOccupant> y)
    {
        return x.Union(y).ToList();
    }

    private static List<IGridOccupant> GetNeighboringOccupants(IGridOccupant occupant, GameBoard gameBoard)
    {
        List<IGridOccupant> matches = new List<IGridOccupant>();
        int x = occupant.x;
        int y = occupant.y;

        matches.Add(gameBoard.GetOccupantAt(x - 1, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x - 1, y));
        matches.Add(gameBoard.GetOccupantAt(x - 1, y + 1));

        matches.Add(gameBoard.GetOccupantAt(x, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x,      y));
        matches.Add(gameBoard.GetOccupantAt(x, y + 1));

        matches.Add(gameBoard.GetOccupantAt(x + 1, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x + 1, y));
        matches.Add(gameBoard.GetOccupantAt(x + 1, y + 1));

        return matches;
    }

    private void OnDestroy()
    {
        gameBoard.AddedOccupantAction -= GameBoard_AddedOccupantAction;
    }
}
