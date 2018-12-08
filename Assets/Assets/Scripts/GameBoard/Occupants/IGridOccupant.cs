using UnityEngine;

public interface IGridOccupant
{
    int x { get; }
    int y { get; }
    GameObject graphic { get; }
    void Slotted(bool value, int x = 0, int y = 0);
}
