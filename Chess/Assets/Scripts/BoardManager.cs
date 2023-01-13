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
        print($"Hit At {positionPiece} -|- Piece Selected {pieceType}");

        AvailableMoves(pieceType, positionPiece);
    }

    void AvailableMoves(string PieceName, Vector2 positionPiece)
    {
        if(PieceName.Equals("Board"))
        {
            insGraphicManager.SpawnMoves(104);
        }

        if(PieceName.Equals("Pawn"))
        {
            Vector2 spawnVector = new Vector2(positionPiece.x,positionPiece.y+1);
            for(int i = 0; i < boardCoordinate.Length; i++)
            {
                if(boardCoordinate[i].x == spawnVector.x 
                        && boardCoordinate[i].y == spawnVector.y)
                    {
                        insGraphicManager.SpawnMoves(i);
                    }
            }
        }
    }
}
