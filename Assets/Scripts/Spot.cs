using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Spot : MonoBehaviour
{
    public int Line;
    public int Column;

    public GameObject ImageCross;
    public GameObject ImageCircle;


    private Symbol _currentSymbol;

    private void Awake()
    {
        GetComponentInParent<BoardController>().RegisterSlot(this);
    }

    public Symbol CurrentSymbol
    {
        set
        {
            _currentSymbol = value;

            ImageCross.SetActive(_currentSymbol == Symbol.X);
            ImageCircle.SetActive(_currentSymbol == Symbol.O);
        }
        get
        {
            return _currentSymbol;
        }
    }

    private void Start()
    {
        CurrentSymbol = Symbol.N;   
    }

    public void SetSymbol(BoardSymbol symbol)
    {
        ImageCross.SetActive(symbol == BoardSymbol.Circle);
        ImageCircle.SetActive(symbol == BoardSymbol.Cross);
    }

    public void CleanSymbol()
    {
        ImageCross.SetActive(false);
        ImageCircle.SetActive(false);
    }
}
