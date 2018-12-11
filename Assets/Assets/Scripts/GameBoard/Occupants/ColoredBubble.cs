using System;
using UnityEngine;

public class ColoredBubble : GridOccupant, IMatchableOccupant
{
    public enum BubbleColors
    {
        INVALID,
        RED,
        //ORANGE,
        YELLOW,
        GREEN,
        BLUE,
        PURPLE
    }
    public BubbleColors color = BubbleColors.INVALID;

    public ColoredBubble(GameObject graphicGO, BubbleColors color = BubbleColors.INVALID) : 
        base(graphicGO)
    {
        SetColor(color);
    }

    public void SetColor(BubbleColors color)
    {
        this.color = color;
        switch (color)
        {
            case BubbleColors.INVALID:
                break;
            case BubbleColors.RED:
                graphic.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case BubbleColors.YELLOW:
                graphic.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case BubbleColors.GREEN:
                graphic.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case BubbleColors.BLUE:
                graphic.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case BubbleColors.PURPLE:
                graphic.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            default:
                break;
        }
    }

    public bool doesMatchWith(IMatchableOccupant otherOccupant)
    {
        if (color != BubbleColors.INVALID && otherOccupant is ColoredBubble && (otherOccupant as ColoredBubble).color == color)
        {
            return true;
        }
        return false;
    }

    public static BubbleColors GetRandomColor()
    {
        var length = Enum.GetValues(typeof(ColoredBubble.BubbleColors)).Length;
        return (BubbleColors)UnityEngine.Random.Range(1, length);
    }

    public override string ToString()
    {
        return base.ToString() + "color : " + color;
    }
}
