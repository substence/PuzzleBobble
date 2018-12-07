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
    void Start () {
        for (int i = 0; i < gameBoard.GetNumberOfCollumns(); i++)
        {
            gameBoard.AddOccupant(new ColoredBubble(testGameObject,ColoredBubble.GetRandomColor()), i, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
