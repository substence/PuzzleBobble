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
            }
            return _graphic;
        }
    }

    public GridOccupant(GameObject graphicGO)
    {
        _graphicGO = graphicGO;
    }

    public void Slotted(bool value, int x = 0, int y = 0)
    {
        _x = x;
        _y = y;
        Lock(value);
    }

    private void Lock(bool value)
    {
        Rigidbody2D body = graphic.GetComponent<Rigidbody2D>();
        body.isKinematic = value;

        Collider2D collider = graphic.GetComponent<Collider2D>();
        collider.isTrigger = value;

        if (value)
        {
            body.velocity = Vector2.zero;
        }
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
