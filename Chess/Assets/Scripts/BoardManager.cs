using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    GraphicManager insGraphicManager; 

    Vector2[] boardIndex = new Vector2[64];
    List<GameObject> pieceStorage = new List<GameObject>(); 
    List<GameObject> movesStorage = new List<GameObject>();
    
        // -- Black Pieces --
    [SerializeField] GameObject whitePawn;
    [SerializeField] GameObject whiteKnight;
    [SerializeField] GameObject whiteBishop;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject whiteQueen;
    [SerializeField] GameObject whiteKing;

    [Space(20)]
        // -- Black Pieces --
    [SerializeField] GameObject blackPawn;
    [SerializeField] GameObject blackKnight;
    [SerializeField] GameObject blackBishop;
    [SerializeField] GameObject blackRook;
    [SerializeField] GameObject blackQueen;
    [SerializeField] GameObject blackKing;

    [Space(20)]
        // -- Move GameObject
    [SerializeField] GameObject movePreview;


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
                boardIndex[slotNumber] = new Vector2(x,y);
                slotNumber++;
            }
        }     

        print("Board Indexed");

        InstantiatePiece();
    }

    void InstantiatePiece()
    {
        Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(28);
            //--Test index 28 --
        GameObject Pawn = Instantiate(whitePawn, vectorLocation, Quaternion.identity);
        Pawn.GetComponent<PieceStorage>().SetIndexData(boardIndex[28]);
        Pawn.GetComponent<PieceStorage>().SetIfInverted(false);
        Pawn.GetComponent<PieceStorage>().SetIfFirstMove(true);
        pieceStorage.Add(Pawn);
    } 

    public void SelectPiece
        (string pieceType, Vector2 indexPosition, bool isInverted, bool isFirstMove)
    {
        if(pieceType.Equals("Board"))
        {

        }
        else if(pieceType.Equals("Move"))
        {

        }
        else
        {
            CalculateMoves(pieceType, indexPosition, isInverted, isFirstMove);
        }
    }

    void DestroyMoves()
    {
        for(int i = 0; i < movesStorage.Count; i ++)
        {
            Destroy(movesStorage[i]);
        }
        movesStorage.Clear();
    }

    void MovePiece()
    {

    } 

    void CalculateMoves
        (string pieceType, Vector2 positionPiece, bool isInverted, bool isFirstMove)
    {


        if(pieceType.Equals("Pawn"))
        {
            if(!isInverted && isFirstMove)
            {   
                Vector2 lastMoveIndex = positionPiece;
                    //-- Pawn Moves 2 Slots if First Move on that piece (-Inverted-)
                for(int i = 0; i < 2; i++)
                {
                    lastMoveIndex.y++;

                    for(int z = 0; z < boardIndex.Length; z++)
                    {
                        if(boardIndex[z].x.Equals(lastMoveIndex.x) &&
                            (boardIndex[z].y.Equals(lastMoveIndex.y)))
                        {
                            Vector3 vectorPos = insGraphicManager.RequestVectorPosition(z);

                            
                        }
                    }
                }
            }
            else if(!isInverted && !isFirstMove)
            {

            }
        }
    }

    void CheckIfLegal(List<move>)
    {

    }
}
