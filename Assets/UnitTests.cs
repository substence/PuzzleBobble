using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTests : MonoBehaviour
{
    [SerializeField]
    private GameBoard gameBoard;
    [SerializeField]
    private GameObject testGameObject;

    // Use this for initialization
    void Start ()
    {
        // fill half the board with random colors and random gaps
        for (int i = 0; i < gameBoard.GetNumberOfCollumns(); i++)
        {
            for (int j = 0; j < gameBoard.GetNumberOfRows() * .5; j++)
            {
                if (Random.Range(0, 100) >= 50)
                {
                    gameBoard.AddOccupant(new ColoredBubble(testGameObject, ColoredBubble.GetRandomColor()), j, i);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
