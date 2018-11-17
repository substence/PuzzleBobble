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
        UpdateStatusText();
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
        GameManagerNetwork.playerChanged.AddListener(PlayerChanged);
    }

    private void PlayerChanged()
    {
        UpdateStatusText();
        AttemptGameStart();
    }

    private void UpdateStatusText()
    {
        String status = "Press button when ready.";
        if (GameManagerNetwork.GetLocalPlayer().isReady)
        {
            status = "Waiting for other players to Ready";
        }
        status += "Players connected(" + GameManagerNetwork.GetNumberOfPlayers() + ")";
        GameStatusText.instance.SetText(status);
    }

    protected virtual void ClickedStartButton()
    {
        startButton.enabled = false;
        GameManagerNetwork.GetLocalPlayer().isReady = true;
        AttemptGameStart();
    }

    private void AttemptGameStart()
    {
        if (GameManagerNetwork.AreAllPlayersReady())
        {
            manager.SwitchState("GameplayState");
        }
        else
        {
            UpdateStatusText();
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
