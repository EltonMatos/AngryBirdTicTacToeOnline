using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum UIScreen
{
    Title,
    LAN,
    Online,
    Lobby,
    Choose,
    Game,
    EndGame
}
public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public bool clientStart = false;
    public bool hostStart = false;


    [Serializable]
    public struct ScreenData
    {
        public UIScreen Screen;
        public GameObject RootObjetc;
    }

    public List<ScreenData> ScreenDatas;

    private Dictionary<UIScreen, GameObject> _screens = new Dictionary<UIScreen, GameObject>();
    private GameObject _activeScreen;

    private void Awake()
    {
        Instance = this;
        foreach (var screenData in ScreenDatas)
        {
            _screens.Add(screenData.Screen, screenData.RootObjetc);

            if(screenData.RootObjetc != null)
            {
                screenData.RootObjetc.SetActive(false);
            }
            
        }
    }

    private void Start()
    {
        GameModeController.Instance.OnHostStarted += OnHostStarted;
        GameModeController.Instance.OnClientStarted += OnClienteStarted;
        GameModeController.Instance.OnClientConnected += OnClienteConnected;

        GoToScreen(UIScreen.Title);
    }
    

    private void OnHostStarted()
    {  
        GoToScreen(UIScreen.Lobby);
    }    

    private void OnClienteStarted()
    {  
        GoToScreen(UIScreen.Game);
    }
    private void OnClienteConnected()
    {
        GoToScreen(UIScreen.Game);
    }
    

    public void GoToScreen(UIScreen screem)
    {
        if(_screens.TryGetValue(screem, out var rootObject))
        {
            if(_activeScreen != null)
            {
                _activeScreen.SetActive(false);
            }

            _activeScreen = rootObject;

            if(rootObject != null)
            {
                rootObject.SetActive(true);
            }            
        }
    }



}
