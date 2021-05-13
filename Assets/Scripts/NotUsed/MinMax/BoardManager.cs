using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficult
{
    easy,
    normal,
    hard,
    pvpLocal,
    pvpOn
}


public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    
    public int BoardSize = 3;
    public Symbol SymbolStart;    
    public List<Symbol> AiPlayers;
    public Difficult level;

    private Symbol _jogadorAtual;
    private Symbol[,] _tabuleiro;
    private Spot[,] _spots; 
    //private int linha, coluna;        

    // 1 = vitoria; 2 = derrota; 3 = empate
    private int statusGame;

    public bool terminou = false;
    //private float modoNormal = 0;


    //network
    private int _currentPlayerIndex;
    private ulong _currentPlayerId => _playerIds[_currentPlayerIndex];
    private readonly List<ulong> _playerIds = new List<ulong>();

    public Spot[,] _slots = new Spot[3, 3];


    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }*/
        Instance = this;

        if (UiManager.instance.getDificuldade() == 2)
        {
            level = Difficult.hard;
        }
        if (UiManager.instance.getDificuldade() == 3)
        {
            level = Difficult.normal;
        }
        if (UiManager.instance.getDificuldade() == 4)
        {
            level = Difficult.pvpLocal;
        }
        if (UiManager.instance.getDificuldade() == 5)
        {
            level = Difficult.pvpOn;
        }

        _tabuleiro = new Symbol[BoardSize, BoardSize];
        _spots = new Spot[BoardSize, BoardSize];

        var allSpots = GetComponentsInChildren<Spot>();
        foreach (var spot in allSpots)
        {
            _spots[spot.Line, spot.Column] = spot;
        }

        //SimboloInicial = getSimboloInicial();
        SymbolStart = Symbol.X;
    }

    private void Start()
    {
        _jogadorAtual = SymbolStart;
        //statusGame = 0;              
        //CheckForAiRound();
    }

    public int getStatus()
    {
        return statusGame;
    }

    public void setStatus(int newStatus)
    {
        statusGame = newStatus; 
    }

    /*public Symbol getSimboloInicial()
    {
        if (FirstPlay.instance.whoPlayer == 1)
        {
            SymbolStart = Symbol.X;
        }
        if (FirstPlay.instance.whoPlayer == 2)
        {
            SymbolStart = Symbol.O;
        }

        return SymbolStart;
    }*/

    /*public void setSimboloInicial(Symbol player)
    {
        _jogadorAtual = player;
    }*/

    public void SpotClicked(Spot spot)
    {
        /*f(terminou == false && GetSymbolAt(spot.Linha, spot.Coluna) == Simbolos.N && level != Difficult.pvpOn)
        {           
            MakePlay(spot.Linha, spot.Coluna);
        }
        else
        {
            MakePlay(_playerIds, spot.Linha, spot.Coluna);
        }*/

        MakePlay(_currentPlayerId, spot.Line, spot.Column);       
        
    }

    public void SetSymbolAt(int line, int column, Symbol symbol)
    {
        _tabuleiro[line, column] = symbol;
        _spots[line, column].CurrentSymbol = symbol;        
    }

    public Symbol GetSymbolAt(int line, int column)
    {
        return _tabuleiro[line, column];
    } 
    
    //makeplay para o multiplayer

    public void AddPlayer(ulong playerID)
    {
        _playerIds.Add(playerID);

        Debug.LogFormat("Player added to the game: {0}", playerID);
    }
    public void MakePlay(ulong playerId, int line, int column)
    {
        Debug.LogFormat("Player {0} wants to make play at {1}, {2}", playerId, line , column);
        
        if (playerId != _currentPlayerId)
        {
            Debug.LogFormat("but it's not their turn! ({0} != {1})", playerId, _currentPlayerIndex);
            return;
        }

        _jogadorAtual = -_currentPlayerIndex == 0 ? Symbol.X : Symbol.O;
        SetSymbolAt(line, column, _jogadorAtual);

        _currentPlayerIndex = 1 - _currentPlayerIndex;
    }

    public void RegisterSlot(Spot slot)
    {
        _slots[slot.Line, slot.Column] = slot;
    }

  

    public static int contarProfundidade(Symbol[,] boardAtual)
    {
        int prof = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (boardAtual[i, j].GetOppositeSymbol() == Symbol.N)
                {
                    prof += 1;
                }
        return prof;
    }

    public Symbol GetWinner()
    {
        // horizontal
        for (int line = 0; line < BoardSize; line++)
        {
            if (_tabuleiro[line, 0] == Symbol.N)
            {
                continue;
            }

            int symbolCount = 0;
            for (int c = 0; c < BoardSize; c++)
            {
                if (_tabuleiro[line, c] == _tabuleiro[line, 0])
                {
                    symbolCount++;
                }
            }

            if (symbolCount == BoardSize)
            {
                return _tabuleiro[line, 0];
            }
        }

        // vertical
        for (int column = 0; column < BoardSize; column++)
        {
            if (_tabuleiro[0, column] == Symbol.N)
            {
                continue;
            }

            int symbolCount = 0;
            for (int l = 0; l < BoardSize; l++)
            {
                if (_tabuleiro[l, column] == _tabuleiro[0, column])
                {
                    symbolCount++;
                }
            }

            if (symbolCount == BoardSize)
            {
                return _tabuleiro[0, column];
            }
        }

        // diagonal
        int diagonalCount1 = 0;
        int diagonalCount2 = 0;
        for (int index = 0; index < BoardSize; index++)
        {
            if (_tabuleiro[index, index] == _tabuleiro[0, 0] && _tabuleiro[0, 0] != Symbol.N)
            {
                diagonalCount1++;
            }
            if (_tabuleiro[index, BoardSize - index - 1] == _tabuleiro[0, BoardSize - 1] && _tabuleiro[0, BoardSize - 1] != Symbol.N)
            {
                diagonalCount2++;
            }
        }
        if (diagonalCount1 == BoardSize)
        {
            return _tabuleiro[0, 0];
        }
        if (diagonalCount2 == BoardSize)
        {
            return _tabuleiro[0, BoardSize - 1];
        }

        return Symbol.N;
    }   
}
