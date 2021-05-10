using MLAPI;
using MLAPI.Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    public LayerMask PickingSlot;
    public Symbol[,] boardSymbol;
	public Symbol newSymbol = Symbol.N;

    public Spot slotServer, slotClient;
    

    private void Start()
    {
        if (IsServer)
        {            
            BoardController.Instance.AddPlayer(OwnerClientId);
            BoardController.Instance.OnUpdateBoard += UpdateBoard;
        }           
    }

    private void UpdateBoard(int line, int column, BoardSymbol symbol)
    {
        //BoardController.Instance.RegisterSlot(slotClient);
        UpdateBoardClientRpc(line, column, symbol);
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);                
                slotServer = hit.transform.GetComponent<Spot>();
                if (hit.collider.CompareTag("PickingSlot") && hit.collider != null)
                {
                    print("Line: " + slotServer.Line + "Column " + slotServer.Column);                    
                    BoardController.Instance.RegisterSlot(slotServer);
                    MakePlayServerRpc(slotServer.Line, slotServer.Column);
                }
            }            
        }
        /*else
        {            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                print("1");
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit2 = Physics2D.Raycast(worldPoint, Vector2.zero);
                slotClient = hit2.transform.GetComponent<Spot>();
                if (hit2.collider.CompareTag("PickingSlot") && hit2.collider != null)
                {                   
                    
                    BoardController.Instance.RegisterSlot(slotClient);
                    
                }
            }
        }*/
    }

   
    [ServerRpc]
    private void MakePlayServerRpc(int line, int column)
    {        
        BoardController.Instance.MakePlay(OwnerClientId, line, column);
    }
    
    [ClientRpc]
    public void UpdateBoardClientRpc(int line, int column, BoardSymbol symbol)
    {
        //BoardController.Instance.RegisterSlot(slotClient);

        BoardController.Instance.UpdateBoardVisuals(line, column, symbol);
    }
}
