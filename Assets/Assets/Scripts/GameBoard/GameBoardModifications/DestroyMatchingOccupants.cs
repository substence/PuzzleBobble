using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyMatchingOccupants : AbstractGameBoardModifier, IGameBoardModifier
{
    [SerializeField]
    private int amountOfMatchesForDestruction = 3;

    public int AmountOfMatchesForDestruction
    {
        get
        {
            return amountOfMatchesForDestruction;
        }

        set
        {
            amountOfMatchesForDestruction = value;
        }
    }

    void Start()
    {
        gameBoard.AddedOccupant += GameBoard_AddedOccupantAction;
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
        int timeout = 500;

        List<IGridOccupant> matches = new List<IGridOccupant>();
        IMatchableOccupant matchableOccupant = occupant as IMatchableOccupant;

        if (matchableOccupant == null)
        {
            return matches;
        }

        List<IGridOccupant> nodesToCheck = GameBoardUtilities.GetNeighboringOccupants(occupant, gameBoard);
        List<IGridOccupant> checkedNodes = new List<IGridOccupant>();
        matches.Add(occupant);
        
        while(nodesToCheck.Count > 0 && timeout > 0)
        {
            timeout--;
            IGridOccupant nodeToCheck = nodesToCheck[0];
            checkedNodes.Add(nodeToCheck);
            nodesToCheck.RemoveAt(0);
            if (nodeToCheck is IMatchableOccupant && (nodeToCheck as IMatchableOccupant).doesMatchWith(matchableOccupant))
            {
                List<IGridOccupant> matchingOccupantsNeighbors = GameBoardUtilities.GetNeighboringOccupants(nodeToCheck, gameBoard);
                matchingOccupantsNeighbors = RemoveOccupantsFoundInList(matchingOccupantsNeighbors, checkedNodes);
                nodesToCheck = mergeListsWithoutDuplicates(nodesToCheck, matchingOccupantsNeighbors);
                matches.Add(nodeToCheck);
            }
        }
        return matches;
    }

    private static List<IGridOccupant> RemoveOccupantsFoundInList(List<IGridOccupant> x, List<IGridOccupant> y)
    {
        for (int i = x.Count - 1; i >= 0; i--)
        {
            IGridOccupant occupant = x[i];
            if (y.Contains(occupant))
            {
                x.Remove(occupant);
            }
        }
        return x;
    }

    private static List<IGridOccupant> mergeListsWithoutDuplicates(List<IGridOccupant> x, List<IGridOccupant> y)
    {
        return x.Union(y).ToList();
    }

    private void OnDestroy()
    {
        gameBoard.AddedOccupant -= GameBoard_AddedOccupantAction;
    }
}
public class GameBoardUtilities
{
    public static List<IGridOccupant> GetNeighboringOccupants(IGridOccupant occupant, GameBoard gameBoard)
    {
        List<IGridOccupant> matches = new List<IGridOccupant>();
        int x = occupant.x;
        int y = occupant.y;

        matches.Add(gameBoard.GetOccupantAt(x - 1, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x - 1, y));
        matches.Add(gameBoard.GetOccupantAt(x - 1, y + 1));

        matches.Add(gameBoard.GetOccupantAt(x, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x, y));
        matches.Add(gameBoard.GetOccupantAt(x, y + 1));

        matches.Add(gameBoard.GetOccupantAt(x + 1, y - 1));
        matches.Add(gameBoard.GetOccupantAt(x + 1, y));
        matches.Add(gameBoard.GetOccupantAt(x + 1, y + 1));

        //todo this will be removed when i cache neighbors
        //remove null opccupants
        for (int i = matches.Count - 1; i >= 0; i--)
        {
            IGridOccupant potentiallyNullOccupant = matches[i];
            if (potentiallyNullOccupant == null)
            {
                matches.Remove(potentiallyNullOccupant);
            }
        }

        return matches;
    }
}
