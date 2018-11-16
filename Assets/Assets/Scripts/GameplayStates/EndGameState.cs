using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameState : PregameState
{
    public override void Enter(AState from)
    {
        base.Enter(from);
        GameStatusText.instance.SetText("Game Over");
        if (startButton)
        {
            Text text = startButton.GetComponentInChildren<Text>();
            if (text)
            {
                text.text = "Start New Game";
            }
        }
    }

    public override string GetName()
    {
        return "EndGameState";
    }

    protected override void ClickedStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        manager.SwitchState("PregameState");
    }

    public override void Tick()
    {
    }
}
