using UnityEngine;

public class GridOccupantTypePool : MonoBehaviour
{
    [SerializeField]
    private GameObject unmatchableBubble;
    [SerializeField]
    private GameObject genericColoredBubble;

    public IGridOccupant GetRandomOccupant()
    {
        return new ColoredBubble(genericColoredBubble, ColoredBubble.GetRandomColor());
    }

    public IGridOccupant GetUnmatchableOccupant()
    {
        return new GridOccupant(unmatchableBubble);
    }
}
