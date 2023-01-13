using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    Vector2[] boardCoordinate = new Vector2[64];

    List<GameObject> movesHolder = new List<GameObject>();

    int selectedPieceSlot;
    string selectedPieceName;


    void Start()
    {
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
    }

    public void SelectedInput(string slotID, string pieceType)
    {
        print($"Hit At {slotID} -|- Piece Selected {pieceType}");

        AvailableMoves(pieceType);
    }

    void AvailableMoves(string PieceName)
    {
        for(int i = 0; i < movesHolder.Count; i++)
        {
            Destroy(movesHolder[i].gameObject);
        }
        movesHolder.Clear();

        if(PieceName.Equals("Pawn"))
        {
            
        }
    }
}
