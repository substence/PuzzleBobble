using UnityEngine;

public class GridOccupant : IGridOccupant
{
    private int _x;
    private int _y;
    private GameObject _graphic;
    [SerializeField]
    private GameObject _graphicGO;

    public GridOccupant(GameObject graphicGO)
    {
        _graphicGO = graphicGO;
    }

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
            }
            return _graphic;
        }
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }
}
