using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    GraphicManager insGraphicManager; 

    Vector2[] boardCoordinate = new Vector2[64];
    

    int selectedPieceSlot;
    string selectedPieceName;


    void Start()
    {
        insGraphicManager = FindObjectOfType<GraphicManager>();

        CoordinateBoard();
    }

    void CoordinateBoard() // -- Index the Board Coordinates --
    {
        int slotNumber = 0;

        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                boardCoordinate[slotNumber] = new Vector2(x,y);
                slotNumber++;
            }
        }     

        print("Board Indexed");
    }

    public void SelectedInput(Vector2 positionPiece, string pieceType)
    {
        
    }

    void AvailableMoves(string PieceName, Vector2 positionPiece)
    {
        
    }
}
