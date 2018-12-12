//Detect floating occupants in the game board and remove them
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingOccupants : AbstractGameBoardModifier, IGameBoardModifier
{
    [SerializeField]
    private float DESTROY_DELAY = 10.0f;

    void Start ()
    {
        //listen when any occupants is removed from the board.
        gameBoard.RemovedOccupants += GameBoard_RemovedOccupant;
	}

    private void GameBoard_RemovedOccupant(List<IGridOccupant> occupants)
    {
        RemoveFloatingOccupants(GetAllUnanchoredOccupants(gameBoard), gameBoard, DESTROY_DELAY);
    }

    public static List<IGridOccupant> GetAllUnanchoredOccupants(GameBoard gameBoard)
    {
        //Get all anchor occupants (occupants attached to the top of the grid)
        List<IGridOccupant> anchors = new List<IGridOccupant>();
        for (int i = 0; i < gameBoard.GetNumberOfCollumns(); i++)
        {
            IGridOccupant occupant = gameBoard.GetOccupantAt(i, 0);
            if (occupant != null)
            {
                anchors.Add(occupant);
            }
        }

        //Get a list of all anchored occupants
        List<IGridOccupant> anchoredOccupants = new List<IGridOccupant>();
        for (int j = 0; j < anchors.Count; j++)
        {
            AddAnchoredOccupantsToList(gameBoard, anchors[j], anchoredOccupants);
        }

        //Get a list of all occupants and remove all anchored occupants from that list, leaving only the unanchored occupants
        List<IGridOccupant> unanchoredOccupants = gameBoard.GetAllValidOccupants();
        for (int ii = unanchoredOccupants.Count - 1; ii >= 0; ii--)
        {
            IGridOccupant occupant = unanchoredOccupants[ii];
            if (occupant == null || anchoredOccupants.Contains(occupant))
            {
                unanchoredOccupants.Remove(occupant);
            }
        }

        return unanchoredOccupants;
    }

    private static void AddAnchoredOccupantsToList(GameBoard gameBoard, IGridOccupant anchor, List<IGridOccupant> anchoredOccupants)
    {
        if (anchor != null && !anchoredOccupants.Contains(anchor))
        {
            anchoredOccupants.Add(anchor);

            List<IGridOccupant> neighbors = GameBoardUtilities.GetNeighboringOccupants(anchor, gameBoard);
            for (int jj = 0; jj < neighbors.Count; jj++)
            {
                IGridOccupant neighbor = neighbors[jj];
                if (neighbor != null && !anchoredOccupants.Contains(neighbor))
                {
                    //anchoredOccupants.Add(neighbor);
                    AddAnchoredOccupantsToList(gameBoard, neighbor, anchoredOccupants);
                }
            }
        }
    }

    //make all the unwanted occupants 'fall' off the grid
    public static void RemoveFloatingOccupants(List<IGridOccupant> occupantsToRemove, GameBoard gameBoard, float  destroyDelay)
    {
        for (int i = 0; i < occupantsToRemove.Count; i++)
        {
            IGridOccupant occupantToRemove = occupantsToRemove[i];
            if (occupantToRemove != null && occupantToRemove.graphic)
            {
                //remove collision so it no longer interacts with other grid occupants
                Collider2D collision = occupantToRemove.graphic.GetComponent<Collider2D>();
                collision.enabled = false;

                //ensure rigidbody is kinematic so it falls with gravity
                Rigidbody2D rigidBody = occupantToRemove.graphic.GetComponent<Rigidbody2D>();
                rigidBody.isKinematic = false;
                rigidBody.gravityScale = 1;

                //Remove from board
                gameBoard.RemoveOccupantAt(occupantToRemove.x, occupantToRemove.y);

                //set a death timer
                Destroy(occupantToRemove.graphic, destroyDelay);
            }
        }
    }

    private void OnDestroy()
    {
        gameBoard.RemovedOccupants -= GameBoard_RemovedOccupant;
    }
}
