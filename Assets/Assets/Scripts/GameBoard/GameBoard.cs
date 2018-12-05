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
        occupant.SetPosition(x, y);
        occupant.graphic.transform.SetParent(transform);
        occupant.graphic.transform.SetPositionAndRotation(GetLocalPositionAt(x,y), occupant.graphic.transform.rotation);
        return true;
    }

    private Vector3 GetLocalPositionAt(int x, int y)
    {
        return new Vector3(x * GRID_CELL_SIZE, y * GRID_CELL_SIZE, 0);
    }

    private void RemoveOccupantAt(int x, int y)
    {
        IGridOccupant existingOccupant = GetOccupantAt(x, y);
        if (existingOccupant != null && existingOccupant.graphic != null)
        {
            existingOccupant.graphic.transform.SetParent(null);
        }
        grid[x, y] = null;
    }

    public IGridOccupant GetOccupantAt(int x, int y)
    {
        return grid[x,y];
    }

    public int GetNumberOfRows(){ return WIDTH; }
    public int GetNumberOfCollumns(){ return HEIGHT; }
}
