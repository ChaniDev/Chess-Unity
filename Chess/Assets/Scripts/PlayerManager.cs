using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    BoardManager insBoardManager; 

    bool WaitForInput = false;

    void Start()
    {
        insBoardManager = FindObjectOfType<BoardManager>();
    }

    public void RequestPlayerInput(int Player) //--Player 1 -(1) \|/ Player 2 -(2)
    {
        WaitForInput = true;
    }

    void Update()
    {
        if(WaitForInput == true)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;

                if(Physics.Raycast(ray, out rayHit))
                {
                    print("Hit");

                    if(rayHit.collider.tag.Equals("Board"))
                    {
                        insBoardManager.SelectedInput
                        (new Vector2(0f,0f), rayHit.collider.tag);
                    }
                    else
                    {
                        Vector2 pieceLocation = rayHit.transform.gameObject
                        .GetComponent<PieceStorage>().GetPosition();

                        insBoardManager.SelectedInput
                        (pieceLocation, rayHit.collider.tag);
                    }
                    
                }
            }
        }
    }
}
