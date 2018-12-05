using UnityEngine;

public interface IGridOccupant
{
    int x { get; }
    int y { get; }
    GameObject graphic { get; }
    void SetPosition(int x, int y);
}
