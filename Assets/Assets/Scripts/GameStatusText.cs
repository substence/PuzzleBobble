using UnityEngine;
using UnityEngine.UI;

//Singleton for text that shows the general state of the game.
public class GameStatusText : MonoBehaviour
{
    static public GameStatusText instance { get { return s_Instance; } }
    static protected GameStatusText s_Instance;
    public Text text;

    protected void OnEnable()
    {
        s_Instance = this;
    }

    public void SetText(string newString)
    {
        text.text = newString;
    }
}
