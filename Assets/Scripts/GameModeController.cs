using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Transports.PhotonRealtime;
using MLAPI.Transports.UNET;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class GameModeController : MonoBehaviour
{
    public static GameModeController Instance { get; private set;}
    
    public event Action OnHostStarted;
    public event Action OnClientStarted;
    public event Action OnClientConnected;

    public event Action OnDisconnected;
    
    //public event Action OnClientDisconnected;

    private void Awake()
    {
        Instance = this;
    }

    public void StartHost()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        
        NetworkManager.Singleton.StartHost();   
        OnDisconnected += StopHost;
        OnHostStarted?.Invoke();
    }

    private void OnClientConnectedCallback(ulong clientId)
    {
        Debug.LogFormat("Client connected: {0} (is server: {1})", clientId, NetworkManager.Singleton.IsServer);        

        if (NetworkManager.Singleton.IsServer)
        {            
            OnClientConnected?.Invoke();            
        }
    }

    public void StartClient(string serverAddress)
    {
        var netManager = NetworkManager.Singleton;
        
        netManager.GetComponent<UNetTransport>().ConnectAddress = serverAddress;
        netManager.StartClient();
        OnDisconnected += StopClient;
        OnClientStarted?.Invoke();
    }
    
    
    public void StartHostPhoton(string roomName)
    {
        var netManager = NetworkManager.Singleton;
        netManager.GetComponent<PhotonRealtimeTransport>().RoomName = roomName;
        
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        NetworkManager.Singleton.StartHost();  
        OnDisconnected += StopHost;
        OnHostStarted?.Invoke();
   
    }
    public void StartClientPhoton(string roomName)
    {
        var netManager = NetworkManager.Singleton;
        netManager.GetComponent<PhotonRealtimeTransport>().RoomName = roomName;
        netManager.StartClient();
        OnDisconnected += StopClient;
        OnClientStarted?.Invoke();
    }

    public void SetPhoton()
    {
        NetworkManager.Singleton.NetworkConfig.NetworkTransport =
            NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();
    }

    public void SetLAN()
    {
        NetworkManager.Singleton.NetworkConfig.NetworkTransport =
            NetworkManager.Singleton.GetComponent<UNetTransport>();
    }
    
    public void StopClient()
    {
        NetworkManager.Singleton.StopClient();
    }
    public void StopHost()
    {
        NetworkManager.Singleton.StopHost();
    }

    public void Disconnect()
    {
        BoardController.Instance.CleanBoard();
        OnDisconnected?.Invoke();
    }

}
