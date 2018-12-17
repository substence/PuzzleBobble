using System;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    enum ControlScheme
    {
        MOUSE,
        KEYBOARD
    }
    [SerializeField]
    private GridOccupantTypePool pool;
    [SerializeField]
    private GameBoard gameBoard;
    [SerializeField]
    private const float force = 1000.0f;
    [SerializeField]
    private ControlScheme scheme = ControlScheme.KEYBOARD;

    private GameObject launchee;
    private static float ROTATION_ANGLE_LIMIT = 45;    
    private static float ROTATION_SPEED = 1;

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
        switch (scheme)
        {
            case ControlScheme.MOUSE:
                UpdateMouse();
                break;
            case ControlScheme.KEYBOARD:
                UpdateKeyboard();
                break;
        }
    }

    private void UpdateKeyboard()
    {
        //direction
        float lowerBound = ROTATION_ANGLE_LIMIT;
        float upperBound = 360 - ROTATION_ANGLE_LIMIT;

        float direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        { 
            direction = Mathf.Round(direction) * -1;
            float amountToRotate = direction * ROTATION_SPEED;

            //clamp rotation
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            currentRotation.z += amountToRotate;
            Debug.Log(currentRotation.z);
            //check if rotation is out of bounds
            if (currentRotation.z > lowerBound && currentRotation.z < upperBound)
            {
                //snap to closest bound
                if (Math.Abs(currentRotation.z - lowerBound) > Math.Abs(currentRotation.z - upperBound))
                {
                    currentRotation.z = upperBound;
                }
                else
                {
                    currentRotation.z = lowerBound;
                }
            }
            //currentRotation.z = Mathf.Max(currentRotation.z, ROTATION_ANGLE_LIMIT);
            //currentRotation.z = Mathf.Min(currentRotation.z, ROTATION_ANGLE_LIMIT);

            //currentRotation.z = Mathf.Clamp(currentRotation.z, -ROTATION_ANGLE_LIMIT, ROTATION_ANGLE_LIMIT);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }

        //fire
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            Fire();
        }
    }

    private void UpdateMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
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
            body.AddForce(gameObject.transform.up * force);
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
        if (gameBoard && (collidedOccupant != null || collidedGameObject == gameBoard.Ceiling))
        {
            IGridOccupant occupant = GridOccupant.GetOccupantFromGameObject(gameObject);
            Vector2 targetPoint = GameBoard.GetClosestNodeToPoint(transform.position);
            gameBoard.AddOccupant(occupant, Mathf.RoundToInt(targetPoint.x), Mathf.RoundToInt(targetPoint.y));
            gameBoard = null;
            Destroy(this);
        }
    }
}
