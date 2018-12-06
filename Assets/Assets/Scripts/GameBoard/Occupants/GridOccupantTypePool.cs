using UnityEngine;

public class GridOccupantTypePool : MonoBehaviour
{
    public GameObject graphicGO;

    public IGridOccupant GetRandomOccupant()
    {
        return new ColoredBubble(graphicGO, ColoredBubble.GetRandomColor());
    }
}
