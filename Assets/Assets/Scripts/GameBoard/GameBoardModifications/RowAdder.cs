using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAdder : AbstractGameBoardModifier, IGameBoardModifier
{
    [SerializeField]
    private float delay = 10.0f;
    [SerializeField]
    private GameBoardQueue queue;
    [SerializeField]
    private GridOccupantTypePool occupantPool;

    private float lastTimeAdded = 0.0f;

    void Start ()
    {
        if (queue == null)
        {
            queue = gameObject.GetComponent<GameBoardQueue>();
        }
        if (occupantPool == null)
        {
            occupantPool = gameObject.GetComponent<GridOccupantTypePool>();
        }
        lastTimeAdded = Time.time;
    }

    private void FixedUpdate()
    {
        float currentTime = Time.time;
        if ((currentTime - lastTimeAdded) > delay)
        {
            AddRow();
            lastTimeAdded = currentTime;
        }        
    }

    private void AddRow()
    {
        for (int i = 0; i < gameBoard.GetNumberOfCollumns(); i++)
        {
            queue.AddToQueue(occupantPool.GetRandomOccupant());
        }
    }
}
public class GameBoardQueue
{
    private List<IGridOccupant> queue;

    public void AddToQueue(List<IGridOccupant> occupants)
    {
        queue.AddRange(occupants);
    }

    public void AddToQueue(IGridOccupant occupant)
    {
        queue.Add(occupant);
    }
}

