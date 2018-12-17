using System.Collections.Generic;

public class GameBoardQueue : AbstractGameBoardModifier,IGameBoardModifier
{
    private List<IGridOccupant> queue = new List<IGridOccupant>();

    /*public void AddToQueue(List<IGridOccupant> occupants)
    {
        queue.AddRange(occupants);
    }*/

    public void AddToQueue(IGridOccupant occupant)
    {
        queue.Add(occupant);
    }

    //todo do this on a timer
    void Update()
    {
        if (queue.Count >= gameBoard.GetNumberOfCollumns())
        {
            InjectRowFromQueueIntoBoard();
        }
    }

    private void InjectRowFromQueueIntoBoard()
    {
        //move everything down
        //List < IGridOccupant > allOccupants = gameBoard.GetAllValidOccupants();
        for (int i = gameBoard.GetNumberOfCollumns() - 1; i >= 0; i--)
        {
            for (int j = gameBoard.GetNumberOfRows() - 1; j >= 0; j--)
            {
                if (gameBoard.GetOccupantAt(j, i) != null)
                {
                    gameBoard.PushDownOccupantAt(j, i);
                }
            }
        }
        for (int ii = 0; ii < gameBoard.GetNumberOfCollumns(); ii++)
        {
            gameBoard.AddOccupant(queue[ii], ii, 0);
        }
        //queue.RemoveRange(0, gameBoard.GetNumberOfCollumns());
        queue.Clear();
    }
}

