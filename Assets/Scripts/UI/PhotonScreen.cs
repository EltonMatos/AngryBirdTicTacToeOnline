using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonScreen : MonoBehaviour
{
    public InputField ServerAddressField;

    public void StartHost()
    {
        GameModeController.Instance.StartHostPhoton(ServerAddressField.text);
        //gameObject.SetActive(false);
    }

    public void StarClient()
    {
        GameModeController.Instance.StartClientPhoton(ServerAddressField.text);
        //gameObject.SetActive(false);
    }
}
