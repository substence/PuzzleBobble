using UnityEngine;

public class GridOccupant : IGridOccupant
{
    private int _x;
    private int _y;
    private GameObject _graphic;
    [SerializeField]
    protected GameObject _graphicGO;

    public int x
    {
        get
        {
            return _x;
        }
    }
    public int y
    {
        get
        {
            return _y;
        }
    }

    public GameObject graphic
    {
        get
        {
            if (_graphic == null)
            {
                _graphic = GameObject.Instantiate(_graphicGO);

                GridOccupantLink link = _graphic.AddComponent<GridOccupantLink>();
                link.gridOccupant = this;

                Rigidbody2D body = _graphic.GetComponent<Rigidbody2D>();
                body.isKinematic = true;
            }
            return _graphic;
        }
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    //Not sure if I want GridOccupant to be a component
    public static IGridOccupant GetOccupantFromGameObject(GameObject gameObject)
    {
        //return gameObject.GetComponent<GridOccupant>();
        GridOccupantLink link = gameObject.GetComponent<GridOccupantLink>();
        if (link)
        {
            return link.gridOccupant;
        }
        return null;
    }
}

//A component put on the graphic's GameObject to link back to the GridOccupant
public class GridOccupantLink : MonoBehaviour
{
    public IGridOccupant gridOccupant;
}
