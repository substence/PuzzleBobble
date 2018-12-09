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
        gameBoard.RemovedOccupant += GameBoard_RemovedOccupant;
	}

    private void GameBoard_RemovedOccupant(IGridOccupant obj)
    {
        RemoveFloatingOccupants(GetAllUnanchoredOccupants(gameBoard), DESTROY_DELAY);
    }

    public static List<IGridOccupant> GetAllUnanchoredOccupants(GameBoard gameBoard)
    {
        List<IGridOccupant> unanchoredOccupants = new List<IGridOccupant>();

        return unanchoredOccupants;
    }
    
    //make all the unwanted occupants 'fall' off the grid
    public static void RemoveFloatingOccupants(List<IGridOccupant> occupantsToRemove, float  destroyDelay)
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

                //set a death timer
                Destroy(occupantToRemove.graphic, destroyDelay);
            }
        }
    }

    private void OnDestroy()
    {
        gameBoard.RemovedOccupant -= GameBoard_RemovedOccupant;
    }
}
