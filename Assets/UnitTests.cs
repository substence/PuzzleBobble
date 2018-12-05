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
            for (int j = 0; j < gameBoard.GetNumberOfRows(); j++)
            {
                gameBoard.AddOccupant(new GridOccupant(testGameObject), i, j);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
