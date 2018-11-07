using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	void Update ()
    {
        if (!isLocalPlayer)
            return;
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
