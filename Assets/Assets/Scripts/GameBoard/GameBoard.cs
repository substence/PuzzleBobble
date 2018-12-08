using System;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private static int WIDTH = 10;
    [SerializeField]
    private static int HEIGHT = 10;
    [SerializeField]
    private static int GRID_CELL_SIZE = 1;

    private IGridOccupant[,] grid = new GridOccupant[WIDTH, HEIGHT];

    public bool AddOccupant(IGridOccupant occupant, int x, int y, bool doesReplaceExistingOccupant = false)
    {
        IGridOccupant existingOccupant = GetOccupantAt(x, y);
        if (existingOccupant != null && !doesReplaceExistingOccupant)
        {
            return false;
        }
        RemoveOccupantAt(x, y);
        grid[x, y] = occupant;
        occupant.Slotted(true, x, y);
        occupant.graphic.transform.SetParent(transform);
        Vector3 localPosition = GetLocalPositionAt(x, y);
        Rigidbody2D rigidBody2D = occupant.graphic.GetComponent<Rigidbody2D>();
        if (rigidBody2D)
        {
            rigidBody2D.MovePosition(localPosition);
        }
        else
        {
            occupant.graphic.transform.SetPositionAndRotation(localPosition, occupant.graphic.transform.rotation);
        }
        return true;
    }

    //special method for moving things down so I can track if something is being pushed off the grid
    public bool PushDownOccupantAt(int x, int y)
    {
        return MoveNodeFromTo(x,y, x, y+1);
    }

    public bool MoveNodeFromTo(int fromX, int fromY, int toX, int toY, bool doesReplaceExistingOccupant = false)
    {
        IGridOccupant occupant = GetOccupantAt(fromX, fromY);
        if (occupant == null)
        {
            return false;
        }
        RemoveOccupantAt(fromX, fromY);
        return AddOccupant(occupant, toX, toY, doesReplaceExistingOccupant);
    }

    private Vector3 GetLocalPositionAt(int x, int y)
    {
        return new Vector3(x * GRID_CELL_SIZE, y * -GRID_CELL_SIZE, 0);
    }

    private void RemoveOccupantAt(int x, int y)
    {
        IGridOccupant existingOccupant = GetOccupantAt(x, y);
        if (existingOccupant != null)
        {
            existingOccupant.Slotted(false);
            if (existingOccupant.graphic != null)
            {
                existingOccupant.graphic.transform.SetParent(null);
            }
        }
        grid[x, y] = null;
    }

    internal static Vector2 GetClosestNodeToPoint(Vector3 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x / GRID_CELL_SIZE), Mathf.RoundToInt(position.y / GRID_CELL_SIZE) * -1);
    }

    public IGridOccupant GetOccupantAt(int x, int y)
    {
        if (x < 0 || y < 0 || x > WIDTH || y > HEIGHT)
        {
            return null;
        }
        //Debug.Log("x " + x + ", y" + y);
        return grid[x,y];
    }

    public int GetNumberOfRows(){ return WIDTH; }
    public int GetNumberOfCollumns(){ return HEIGHT; }
}
