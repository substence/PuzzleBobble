using System.Collections.Generic;
using UnityEngine;

public class WinGameWhenOccupantCountIsBelowThreshold : AbstractGameBoardModifier, IGameBoardModifier
{
    [SerializeField]
    private int minOccupantThreshold = 0;
    
    void Start ()
    {        
        //listen when any occupants is removed from the board.
        gameBoard.RemovedOccupants += GameBoard_RemovedOccupant;
	}

    private void GameBoard_RemovedOccupant(List<IGridOccupant> occupants)
    {
        if (gameBoard.GetAllValidOccupants().Count <= minOccupantThreshold)
        {
            GameManager.instance.EndGame(new EndGameParameters(this, "You win! Board Clear"));
        }
    }

    private void OnDestroy()
    {
        gameBoard.RemovedOccupants -= GameBoard_RemovedOccupant;
    }
}
