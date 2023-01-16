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

                
            }
        }
    }
}
