using UnityEngine;

[RequireComponent(typeof(GridOccupantTypePool))]
public class DestroyFloatingOccupantsTestPreset : GameBoardPreset
{
    void Start ()
    {
        GridOccupantTypePool occupantPool = gameObject.GetComponent<GridOccupantTypePool>();
        if (occupantPool == null)
        {
            //throw error
            return;
        }
        
        //Create a few bubbles in the center of the board
        for (int i = 0; i < gameBoard.GetNumberOfRows() * .5; i++)
        {
            gameBoard.AddOccupant(occupantPool.GetRandomOccupant(), 5, i);
        }

        //Require only 1 match to destroy bubbles to make it easier for testing
        DestroyMatchingOccupants modifier = gameObject.GetComponent<DestroyMatchingOccupants>();
        if (modifier != null)
        {
            modifier.AmountOfMatchesForDestruction = 1;
        }
    }
}
