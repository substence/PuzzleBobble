using System;
using UnityEngine;
using UnityEngine.UI;

public class PregameState : AState
{
    public GameObject UIGO;
    private GameObject UIGOInstance;
    protected Button startButton;

    public override void Enter(AState from)
    {
        //GameStatusText.instance.SetText("Waiting to start game");
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas)
        {
            UIGOInstance = GameObject.Instantiate<GameObject>(UIGO);
            UIGOInstance.transform.SetParent(canvas.transform, false);
            startButton = UIGOInstance.GetComponentInChildren<Button>();
            if (startButton)
            {
                Text text = startButton.GetComponentInChildren<Text>();
                if (text)
                {
                    text.text = "Start Game";
                }
                startButton.onClick.AddListener(ClickedStartButton);
            }
        }
    }

    protected virtual void ClickedStartButton()
    {
        startButton.enabled = false;
        GameManagerNetwork.GetLocalPlayer().isReady = true;
        if (GameManagerNetwork.AreAllPlayersReady())
        {
            manager.SwitchState("GameplayState");
        }
        else
        {
            GameStatusText.instance.SetText("Waiting for all players to be ready");
        }
    }

    public override void Exit(AState to)
    {
        if(UIGOInstance)
        {
            GameObject.Destroy(UIGOInstance);
        }
        if (startButton)
        {
            startButton.onClick.RemoveListener(ClickedStartButton);
        }
        //throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "PregameState";
    }

    public override void Tick()
    {
        //throw new System.NotImplementedException();
    }
}
