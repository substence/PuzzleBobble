using UnityEngine;

public class EndGameWhenOverflowing : AbstractGameBoardModifier , IGameBoardModifier
{
    [SerializeField]
    private int overflowThreshold = 0;

    void Start()
    {
        //listen when any occupants is removed from the board.
        gameBoard.PushedOccupant += GameBoard_PushedOccupant;
    }

    private void GameBoard_PushedOccupant(int toX, int toY)
    {
       if ((toX + overflowThreshold) >= gameBoard.GetNumberOfRows())
        {
            GameManager.instance.EndGame(new EndGameParameters(this, "Game Over!"));
        }
    }

    private void OnDestroy()
    {
        gameBoard.PushedOccupant -= GameBoard_PushedOccupant;
    }
}
