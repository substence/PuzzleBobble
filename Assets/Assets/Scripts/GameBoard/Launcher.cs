using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private GridOccupantTypePool pool;
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
        Rigidbody2D body = launchee.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        Collider2D collider = launchee.GetComponent<Collider2D>();
        collider.isTrigger = true;
        launchee.transform.SetParent(transform, false);
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
            Rigidbody2D body = launchee.GetComponent<Rigidbody2D>();
            body.isKinematic = false;
            body.AddForce(gameObject.transform.up * 1000.0f);
            //body.AddForce(Vector2.up * 1000.0f);

            Collider2D collider = launchee.GetComponent<Collider2D>();
            collider.isTrigger = false;
            Load();
        }
    }
}
