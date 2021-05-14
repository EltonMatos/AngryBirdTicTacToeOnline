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
    public Action<BoardSymbol> OnMatchEnd; 

    private Spot[,] _slots = new Spot[3, 3];
    private BoardSymbol[,] _board = new BoardSymbol[3, 3];

    private int BoardSize = 3;

    public int statusFinalGame = 0;
    public bool statusGame = true;

    private int _numberOfPlays;
    
    private void Awake()
    {
        Instance = this;
        _numberOfPlays = 0;
    }

    public void CleanBoard()
    {
        _numberOfPlays = 0;
        CleanSlots();
        _board = new BoardSymbol[3, 3];
        OnUpdateBoard = null;
        OnMatchEnd = null;
    }

    public void CleanSlots()
    {
        for (int line = 0; line < BoardSize; line++) {
            for (int column = 0; column < BoardSize; column++)
            {
                _slots[line, column].CleanSymbol();
            }
        }
    }

    public void RegisterSlot(Spot slot)
    {
        _slots[slot.Line, slot.Column] = slot;
    }    

    public void MakePlay(ulong playerId, int line, int column)
    {
        if(statusGame == true)
        {
            Debug.LogFormat("Player {0} wants to make play at {1}, {2}", playerId, line, column);

            if (playerId != PlayerController.Instance._currentPlayerId)
            {
                Debug.LogFormat("But it1s not their turn! ({0} != {1})", playerId, PlayerController.Instance._currentPlayerId);
                return;
            }

            if (_board[line, column] != BoardSymbol.None)
            {
                Debug.LogFormat("But the space is not empty! (Current: {0})", _board[line, column]);
                return;
            }

            BoardSymbol symbolToSet = PlayerController.Instance._currentPlayerIndex == 0 ? BoardSymbol.Circle : BoardSymbol.Cross;
            OnUpdateBoard?.Invoke(line, column, symbolToSet);

            _board[line, column] = symbolToSet;

            PlayerController.Instance._currentPlayerIndex = 1 - PlayerController.Instance._currentPlayerIndex;
        }

        var winner = GetWinner();
        if(winner == BoardSymbol.Circle)
        {
            print("Bird vencedor");
            OnMatchEnd?.Invoke(BoardSymbol.Circle); 
        }
        if (winner == BoardSymbol.Cross)
        {
            print("Pig vencedor");
            OnMatchEnd?.Invoke(BoardSymbol.Cross);
        }
        if (winner == BoardSymbol.None)
        {
            print("Ninguem venceu");
        }
        
        _numberOfPlays += 1;
        if (_numberOfPlays == BoardSize * BoardSize)
        {
            OnMatchEnd?.Invoke(BoardSymbol.None);
        }

    }

    //Method only called after knowing if the game is already over;
    public bool IWin(ulong playerId, BoardSymbol winnerSymbol)
    {
        BoardSymbol mySymbol = PlayerController.Instance._playerIds.IndexOf(playerId) == 0 ? BoardSymbol.Circle : BoardSymbol.Cross;
        if (mySymbol == winnerSymbol)
        {
            return true;
        }

        return false;
    }

    public void MatchEnd(ulong playerId, BoardSymbol winnerSymbol)
    {
        _numberOfPlays = 0;  
        EndScreen endScreen = UIController.Instance.GetScreen(UIScreen.EndGame).GetComponent<EndScreen>();
        if (winnerSymbol == BoardSymbol.None)
        {
            endScreen.ShowDraw();
        }
        else if (IWin(playerId, winnerSymbol))
        {
            endScreen.ShowWin();
        }
        else
        {
            endScreen.ShowLose();
        }

        UIController.Instance.GoToScreen(UIScreen.EndGame);
        
    }

    public void UpdateBoardVisuals(int line, int column, BoardSymbol symbol)
    {
        _slots[line, column].SetSymbol(symbol);
    }    

    public BoardSymbol GetWinner()
    {
        // horizontal
        for (int line = 0; line < BoardSize; line++)
        {
            if (_board[line, 0] == BoardSymbol.None)
            {
                continue;
            }

            int symbolCount = 0;
            for (int c = 0; c < BoardSize; c++)
            {
                if (_board[line, c] == _board[line, 0])
                {
                    symbolCount++;
                }
            }

            if (symbolCount == BoardSize)
            {
                return _board[line, 0];
            }
        }

        // vertical
        for (int column = 0; column < BoardSize; column++)
        {
            if (_board[0, column] == BoardSymbol.None)
            {
                continue;
            }

            int symbolCount = 0;
            for (int l = 0; l < BoardSize; l++)
            {
                if (_board[l, column] == _board[0, column])
                {
                    symbolCount++;
                }
            }

            if (symbolCount == BoardSize)
            {
                return _board[0, column];
            }
        }

        // diagonal
        int diagonalCount1 = 0;
        int diagonalCount2 = 0;
        for (int index = 0; index < BoardSize; index++)
        {
            if (_board[index, index] == _board[0, 0] && _board[0, 0] != BoardSymbol.None)
            {
                diagonalCount1++;
            }
            if (_board[index, BoardSize - index - 1] == _board[0, BoardSize - 1] && _board[0, BoardSize - 1] != BoardSymbol.None)
            {
                diagonalCount2++;
            }
        }
        if (diagonalCount1 == BoardSize)
        {
            return _board[0, 0];
        }
        if (diagonalCount2 == BoardSize)
        {
            return _board[0, BoardSize - 1];
        }
        
        

        
        return BoardSymbol.None;
    }


}
