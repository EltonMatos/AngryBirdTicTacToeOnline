using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardSymbol
{
    None,
    Cross,
    Circle
}

public class BoardController : MonoBehaviour
{
    public static BoardController Instance { get; private set; }

    public Action<int, int, BoardSymbol> OnUpdateBoard;

    private int _currentPlayerIndex;
    private ulong _currentPlayerId => _playerIds[_currentPlayerIndex];
    private readonly List<ulong> _playerIds = new List<ulong>();

    private Spot[,] _slots = new Spot[3, 3];
    private BoardSymbol[,] _board = new BoardSymbol[3, 3];

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterSlot(Spot slot)
    {
        _slots[slot.Line, slot.Column] = slot;
    }

    public void AddPlayer(ulong playerID)
    {        
        _playerIds.Add(playerID);

        Debug.LogFormat("Player added to the game: {0}", playerID);
    }

    public void MakePlay(ulong playerId, int line, int column)
    {
        Debug.LogFormat("Player {0} wants to make play at {1}, {2}", playerId, line, column);

        if (playerId != _currentPlayerId)
        {
            Debug.LogFormat("But it1s not their turn! ({0} != {1})", playerId, _currentPlayerId);
            return;
        }

        if(_board[line, column] != BoardSymbol.None)
        {
            Debug.LogFormat("But the space is not empty! (Current: {0})", _board[line, column]);
            return;
        }

        BoardSymbol symbolToSet = _currentPlayerIndex == 0 ? BoardSymbol.Circle : BoardSymbol.Cross;
        //_slots[line, column].SetSymbol(symbolToSet);
        OnUpdateBoard?.Invoke(line, column, symbolToSet);

        _board[line, column] = symbolToSet;

        _currentPlayerIndex = 1 - _currentPlayerIndex;
    }

    public void UpdateBoardVisuals(int line, int column, BoardSymbol symbol)
    {
        _slots[line, column].SetSymbol(symbol);
    }


}
