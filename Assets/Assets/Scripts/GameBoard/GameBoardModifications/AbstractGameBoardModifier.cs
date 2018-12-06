using UnityEngine;

public class AbstractGameBoardModifier : MonoBehaviour
{
    [SerializeField]
    protected GameBoard gameBoard;

    void Start()
    {
        if (gameBoard == null)
        {
            gameBoard = gameObject.GetComponent<GameBoard>();
        }
    }
}

