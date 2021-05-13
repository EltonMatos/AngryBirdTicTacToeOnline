using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text WinMessage;
    public Text LoseMessage;
    public Text DrawMessage;


    public void Update()
    {
        if (Input.anyKey)
        {
            GameModeController.Instance.Disconnect();
        }
    }

    public void ShowWin()
    {
        WinMessage.enabled = true;
        LoseMessage.enabled = false;
        DrawMessage.enabled = false;
    }
    public void ShowLose()
    {
        WinMessage.enabled = false;
        LoseMessage.enabled = true;
        DrawMessage.enabled = false;
        
    }
    
    public void ShowDraw()
    {
        WinMessage.enabled = false;
        LoseMessage.enabled = false;
        DrawMessage.enabled = true;
    }
    
}
