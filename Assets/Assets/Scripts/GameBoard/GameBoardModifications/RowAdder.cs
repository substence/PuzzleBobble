using UnityEngine;

[RequireComponent(typeof(GameBoardQueue), typeof(GridOccupantTypePool), typeof(GameBoard))]
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
