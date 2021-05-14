using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonScreen : MonoBehaviour
{
    public InputField ServerAddressField;

    public void StartHost()
    {
        
        Debug.Log(ServerAddressField.text.Length);
        if (ServerAddressField.text.Length > 0)
        {
            GameModeController.Instance.StartHostPhoton(ServerAddressField.text);
        }
        else
        {
            Debug.Log("Please, fill the Room Name");
        }


    }

    public void StarClient()
    {
        if (ServerAddressField.text.Length > 0)
        {
            GameModeController.Instance.StartClientPhoton(ServerAddressField.text);
        }
        else
        {
            Debug.Log("Please, fill the Room Name");
        }
    }
}
