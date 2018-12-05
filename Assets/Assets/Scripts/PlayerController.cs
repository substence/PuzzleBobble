using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public static UnityEvent readyStateChanged = new UnityEvent();
    [SyncVar]
    private bool _isReady = false;
    public bool isReady
    {
        get { return _isReady; }
        set
        {
            _isReady = value;
            //CmdReadyStateChanges(value);
            //readyStateChanged.Invoke();
        }
    }

    [Command]
    void CmdReadyStateChanges(bool value)
    {
        //_isReady = value;
        //readyStateChanged.Invoke();
    }

    private void Start()
    {
        GameManager.instance.PushState("PregameState");
    }

    void Update ()
    {
        
        if (!isLocalPlayer)
            return;
        //Debug.Log(GameManager.instance.topState);
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        transform.Translate(x, z, 0);
    }

    public override void OnStartLocalPlayer()
    {
        GameObject GM = GameManager.instance.gameObject;
        GameManagerNetwork GMN = GM.GetComponent<GameManagerNetwork>();
        if (GMN)
        {
            GMN.localPlayerGO = this.gameObject;
        }
    }
}
