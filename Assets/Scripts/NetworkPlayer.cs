using MLAPI;
using MLAPI.Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    public LayerMask PickingSlot;    

    public Spot slot;
    

    private void Start()
    {
        if (IsServer)
        {
            PlayerController.Instance.AddPlayer(OwnerClientId);
            BoardController.Instance.OnUpdateBoard += UpdateBoard;
        }           
    }

    private void UpdateBoard(int line, int column, BoardSymbol symbol)
    {        
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
                slot = hit.transform.GetComponent<Spot>();                
                if (hit.collider.CompareTag("PickingSlot"))
                {  
                    MakePlayServerRpc(slot.Line, slot.Column);
                }
            }            
        }        
    }

   
    [ServerRpc]
    private void MakePlayServerRpc(int line, int column)
    {        
        BoardController.Instance.MakePlay(OwnerClientId, line, column);
    }
    
    [ClientRpc]
    public void UpdateBoardClientRpc(int line, int column, BoardSymbol symbol)
    {
        BoardController.Instance.UpdateBoardVisuals(line, column, symbol);
    }
}
