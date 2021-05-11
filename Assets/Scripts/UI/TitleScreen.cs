using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void LANButton()
    {
        UIController.Instance.GoToScreen(UIScreen.LAN);
    }

    // Update is called once per frame
    public void OnlineButton()
    {
        UIController.Instance.GoToScreen(UIScreen.Online);
    }

}
