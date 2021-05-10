using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Symbol
{
    N,
    X,
    O
}
public static class ExtensaoSimbolos
{
    public static Symbol GetOppositeSymbol(this Symbol symbol)
    {
        switch (symbol)
        {
            case Symbol.X: return Symbol.O;
            case Symbol.O: return Symbol.X;
            default: return Symbol.N;
        }
    }

    static void Example()
    {
        Symbol x = Symbol.O;        
        Symbol other1 = x.GetOppositeSymbol();
    }
}
