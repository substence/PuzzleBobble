using UnityEngine;

public class GridOccupantTypePool : MonoBehaviour
{
    [SerializeField]
    private GameObject testGameObject;

    public IGridOccupant GetRandomOccupant()
    {        
        return new GridOccupant(Instantiate(testGameObject));
    }
}

