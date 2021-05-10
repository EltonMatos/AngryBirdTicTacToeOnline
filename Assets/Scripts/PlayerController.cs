using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    public int _currentPlayerIndex;
    public ulong _currentPlayerId => _playerIds[_currentPlayerIndex];
    public readonly List<ulong> _playerIds = new List<ulong>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayer(ulong playerID)
    {
        _playerIds.Add(playerID);

        Debug.LogFormat("Player added to the game: {0}", playerID);
    }
}
