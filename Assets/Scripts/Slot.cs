using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int Line;
    public int Column;

    public GameObject ImageCross;
    public GameObject ImageCircle;


    private void Awake()
    {
        //GetComponentInParent<BoardController>().RegisterSlot(this);
    }

    public void SetSymbol(BoardSymbol symbol)
    {
        ImageCross.SetActive(symbol == BoardSymbol.Circle);
        ImageCircle.SetActive(symbol == BoardSymbol.Cross);
    }
}
