using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private GridOccupantTypePool pool;
    [SerializeField]
    private GameBoard gameBoard;
    private GameObject launchee;

    void Start()
    {
        if (pool == null)
        {
            pool = gameObject.GetComponent<GridOccupantTypePool>();
        }
        Load();
    }

    void Load()
    {
        launchee = pool.GetRandomOccupant().graphic;
        SetKinematicAndTrigger(launchee, true);
        launchee.transform.SetParent(pool.gameObject.transform);
        launchee.transform.SetPositionAndRotation(transform.position, launchee.transform.rotation);
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, 
                                        mousePosition.y - transform.position.y);
        transform.up = direction;
	    if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
	}

    void Fire()
    {
        if (launchee)
        {
            SetKinematicAndTrigger(launchee, false);
            Rigidbody2D body = launchee.GetComponent<Rigidbody2D>();
            body.AddForce(gameObject.transform.up * 1000.0f);
            SnapToGridOnCollision stopOnCollision = launchee.AddComponent<SnapToGridOnCollision>();
            stopOnCollision.gameBoard = gameBoard;
            Load();
        }
    }

    void SetKinematicAndTrigger(GameObject target, bool value)
    {
        Rigidbody2D body = target.GetComponent<Rigidbody2D>();
        body.isKinematic = value;
        Collider2D collider = target.GetComponent<Collider2D>();
        //collider.isTrigger = value;
        collider.enabled = !value;
    }
}
class SnapToGridOnCollision : MonoBehaviour
{
    public GameBoard gameBoard;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnCollision(collider.gameObject);
    }

    private void OnCollision(GameObject collidedGameObject)
    {
        IGridOccupant collidedOccupant = GridOccupant.GetOccupantFromGameObject(collidedGameObject);
        if (collidedOccupant != null && gameBoard)
        {
            IGridOccupant occupant = GridOccupant.GetOccupantFromGameObject(gameObject);
            Vector2 targetPoint = GameBoard.GetClosestNodeToPoint(transform.position);
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.velocity = Vector2.zero;
            body.isKinematic = true;
            //targetPoint = new Vector2(Mathf.RoundToInt(targetPoint.x), Mathf.RoundToInt(targetPoint.y));
            //body.MovePosition(new Vector2(targetPoint.x, targetPoint.y));
            gameBoard.AddOccupant(occupant, Mathf.RoundToInt(targetPoint.x), Mathf.RoundToInt(targetPoint.y));
            gameBoard = null;
            Destroy(this);
        }
    }
}
