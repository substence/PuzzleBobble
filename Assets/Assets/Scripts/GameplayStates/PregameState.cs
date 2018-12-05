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
        PuzzleBobbleNetworkManager.playerChanged.AddListener(PlayerStateChanged);
        PlayerController.readyStateChanged.AddListener(PlayerStateChanged);
    }

    private void PlayerStateChanged()
    {
        UpdateStatusText();
        AttemptGameStart();
    }

    private void UpdateStatusText()
    {
        String status = "Press button when ready.";
        if (PuzzleBobbleNetworkManager.GetLocalPlayer().isReady)
        {
            status = "Waiting for other players to Ready";
        }
        status += "Players connected(" + PuzzleBobbleNetworkManager.GetNumberOfPlayers() + ")";
        GameStatusText.instance.SetText(status);
    }

    protected virtual void ClickedStartButton()
    {
        startButton.enabled = false;
        PuzzleBobbleNetworkManager.GetLocalPlayer().isReady = true;
        AttemptGameStart();
    }

    private void AttemptGameStart()
    {
        if (PuzzleBobbleNetworkManager.AreAllPlayersReady())
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
        PuzzleBobbleNetworkManager.playerChanged.RemoveListener(PlayerStateChanged);
        PlayerController.readyStateChanged.RemoveListener(PlayerStateChanged);
        //throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "PregameState";
    }

    public override void Tick()
    {
        AttemptGameStart();
        Debug.Log("pregame tick");
        //throw new System.NotImplementedException();
    }
}
