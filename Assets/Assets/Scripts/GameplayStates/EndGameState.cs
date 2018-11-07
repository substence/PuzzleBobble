using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EndGameState : AState
{
    public override void Enter(AState from)
    {
        GameStatusText.instance.SetText("Game Over");
    }

    public override void Exit(AState to)
    {
        //throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "EndGameState";
    }

    public override void Tick()
    {
    }
}
