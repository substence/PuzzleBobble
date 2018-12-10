using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardPreset : AbstractGameBoardModifier , IGameBoardModifier
{
    void Start()
    {
        for (int i = 0; i < gameBoard.GetNumberOfCollumns(); i++)
        {
            for (int j = 0; j < gameBoard.GetNumberOfRows(); j++)
            {
                //gameBoard.AddOccupant(new ColoredBubble(testGameObject, ColoredBubble.GetRandomColor()), j, i);
            }
        }
    }
}
