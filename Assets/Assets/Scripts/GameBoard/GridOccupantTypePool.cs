﻿using UnityEngine;
using System;

public class GridOccupantTypePool : MonoBehaviour
{
    public GameObject graphicGO;

    public IGridOccupant GetRandomOccupant()
    {
        return new ColoredBubble(graphicGO, ColoredBubble.GetRandomColor());
    }
}
public class ColoredBubble : GridOccupant, ILinkableOccupant
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

    public ColoredBubble(GameObject graphicGO, BubbleColors color = BubbleColors.INVALID)
    {
        _graphicGO = graphicGO;
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

    public bool doesLinkWith(ILinkableOccupant otherOccupant)
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
}
public interface ILinkableOccupant
{
    bool doesLinkWith(ILinkableOccupant otherOccupant);
}
