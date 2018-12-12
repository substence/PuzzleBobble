using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public event Action<IGridOccupant> AddedOccupant;
    public event Action<List<IGridOccupant>> RemovedOccupants;
    public event Action<int, int> PushedOccupant;

    [SerializeField]
    private static int WIDTH = 10;
    [SerializeField]
    private static int HEIGHT = 10;
    [SerializeField]
    private static int GRID_CELL_SIZE = 1;
    [SerializeField]
    private GameObject ceiling;
    public GameObject Ceiling
    {
        get
        {
            return ceiling;
        }
    }

    private IGridOccupant[,] grid = new GridOccupant[WIDTH, HEIGHT];
    private List<IGridOccupant> recentlyRemovedOccupants = new List<IGridOccupant>();

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
        if (AddedOccupant != null)
        {
            AddedOccupant(occupant);
        }
        return true;
    }

    //special method for moving things down so I can track if something is being pushed off the grid
    public bool PushDownOccupantAt(int x, int y)
    {
        int toX = x;
        int toY = y+1;
        bool moved = MoveNodeFromTo(x,y, toX, toY);
        if (PushedOccupant != null)
        {
            PushedOccupant(toX, toY);
        }
        return moved;
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

    public void RemoveOccupantAt(int x, int y)
    {
        if (IsIndexOutOfBounds(x,y))
        {
            return;
        }
        IGridOccupant existingOccupant = GetOccupantAt(x, y);
        if (existingOccupant != null)
        {
            existingOccupant.Slotted(false);
            if (existingOccupant.graphic != null)
            {
                existingOccupant.graphic.transform.SetParent(null);
            }
            recentlyRemovedOccupants.Add(existingOccupant);
        }
        grid[x, y] = null;
    }

    private bool IsIndexOutOfBounds(int x, int y)
    {
        return x < 0 || y < 0 || x >= WIDTH || y >= HEIGHT;
    }

    internal static Vector2 GetClosestNodeToPoint(Vector3 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x / GRID_CELL_SIZE), Mathf.RoundToInt(position.y / GRID_CELL_SIZE) * -1);
    }

    public IGridOccupant GetOccupantAt(int x, int y)
    {
        if (IsIndexOutOfBounds(x,y))
        {
            return null;
        }
        //Debug.Log("x " + x + ", y" + y);
        return grid[x,y];
    }

    public List<IGridOccupant> GetAllValidOccupants()
    {
        List<IGridOccupant> occupants = new List<IGridOccupant>();

        for (int i = 0; i < WIDTH; i++)
        {
            for (int j = 0; j < HEIGHT; j++)
            {
                IGridOccupant occupant = GetOccupantAt(i, j);
                if (occupant != null)
                {
                    occupants.Add(occupant);
                }
            }
        }

        return occupants;
    }

    private void Update()
    {
        AnnounceRemovedOccupants();
    }
    
    //At the end of every frame, announce all the occupants that were removed this frame
    private void AnnounceRemovedOccupants()
    {
        if (recentlyRemovedOccupants.Count > 0 && RemovedOccupants != null)
        {
            RemovedOccupants(recentlyRemovedOccupants);
            recentlyRemovedOccupants.Clear();
        }
    }

    public int GetNumberOfRows(){ return WIDTH; }
    public int GetNumberOfCollumns(){ return HEIGHT; }
}
