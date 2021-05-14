using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonReturnToStart()
    {
        GameModeController.Instance.Disconnect();
    }

    // Update is called once per frame
    public void ButtonReloadMatch()
    {
        BoardController.Instance.CleanBoard();
        PlayerController.Instance.InitSetCurrentPlayer();
    }
}
