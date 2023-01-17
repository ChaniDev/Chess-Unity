using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    GraphicManager insGraphicManager; 
    PieceMoveset insPieceMoveset;

    Vector2[] boardIndex = new Vector2[64];
    List<GameObject> pieceStorage = new List<GameObject>(); 
    List<Vector3> possibleMoves = new List<Vector3>();
    List<GameObject> moveStorage = new List<GameObject>();
    
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

    [SerializeField] int? selectedPiece = null; 


    void Start()
    {
        insGraphicManager = FindObjectOfType<GraphicManager>();
        insPieceMoveset = FindObjectOfType<PieceMoveset>();

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
        int[] whiteSpawnArray = new int[]{28,28+5,28+11};

        foreach(int i in whiteSpawnArray)
        {
            Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(i);
                //--Test index i --
            GameObject Pawn = Instantiate(whitePawn, vectorLocation, Quaternion.identity);
            Pawn.GetComponent<PieceStorage>().SetIndexData(boardIndex[i]);
            Pawn.GetComponent<PieceStorage>().SetIfInverted(false);
            Pawn.GetComponent<PieceStorage>().SetIfFirstMove(false);
            pieceStorage.Add(Pawn);
        }

        int[] blackSpawnArray = new int[]{0,1,28+8,28+16,28+18,28+14};

        foreach(int i in blackSpawnArray)
        {
            Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(i);
                //--Test index i --
            GameObject Pawn = Instantiate(blackPawn, vectorLocation, Quaternion.identity);
            Pawn.GetComponent<PieceStorage>().SetIndexData(boardIndex[i]);
            Pawn.GetComponent<PieceStorage>().SetIfInverted(true);
            Pawn.GetComponent<PieceStorage>().SetIfFirstMove(true);
            pieceStorage.Add(Pawn);
        }
    } 

    public void SelectPiece
        (
            string pieceType, Vector2 indexPosition, bool isInverted, 
            bool isFirstMove, bool isWhite
        )
    {
        if(pieceType.Equals("Board"))
        {
            DestroyMoves();

            selectedPiece = null;
        }
        else if(pieceType.Equals("Move"))
        {
            MovePiece(indexPosition);
        }
        else
        {
            for(int i = 0; i < pieceStorage.Count; i++)
            {
                if(pieceStorage[i].GetComponent<PieceStorage>().GetIndexData()
                    .Equals(indexPosition))
                {
                    selectedPiece = i;
                }
            }

            insPieceMoveset.CalculateMoves
                (
                    pieceType, indexPosition, isInverted, isFirstMove, 
                    boardIndex, pieceStorage, possibleMoves, isWhite
                );
        }
    }

    public void DestroyMoves()
    {
        for(int i = 0; i < moveStorage.Count; i ++)
        {
            Destroy(moveStorage[i]);
        }
        moveStorage.Clear();

        possibleMoves.Clear();
    }

    public void CheckIfLegal(List<Vector3> possibleMoves)
    {
        // !!! -- Check if move is Legal -- !!!

        DrawLegalMoves(possibleMoves);
    }

    void DrawLegalMoves( List<Vector3> possibleMoves)
    {
        int vectorIndex = 0;

        for(int i = 0; i < possibleMoves.Count; i ++)
        {
            for(int j = 0; j < boardIndex.Length; j++)
            {
                if(possibleMoves[i].Equals(boardIndex[j]))
                {
                    vectorIndex = j;
                }
            }

            Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(vectorIndex);

            GameObject move = Instantiate(movePreview, vectorLocation, Quaternion.identity); 
            move.GetComponent<PieceStorage>().SetIndexData(possibleMoves[i]);
            moveStorage.Add(move);
        }
    }

    void MovePiece(Vector2 indexPosition)
    {
        Vector2 tmpIndexPosition = indexPosition;

            if(selectedPiece.Equals(null))
            {
                DestroyMoves();
                return;
            }
            
            pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
                .SetIfFirstMove(false);
            
            for(int i = 0; i < pieceStorage.Count; i++)
            {
                if(pieceStorage[i].GetComponent<PieceStorage>().GetIndexData()
                    .Equals(indexPosition))
                {
                    Destroy(pieceStorage[i]);
                    pieceStorage.RemoveAt(i);
                }
            }

            pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
                .SetIndexData(tmpIndexPosition);

            Vector3 vectorPosition = new Vector3(0,0,0);

            for(int i = 0; i < boardIndex.Length; i++)
            {
                if(boardIndex[i].Equals(tmpIndexPosition))
                {
                    vectorPosition = insGraphicManager.RequestVectorPosition(i);
                }
            }

            pieceStorage[(int)selectedPiece].transform.position = vectorPosition;

            DestroyMoves();
            

            // !!! -- Scan the board for check -- !!!
    } 
}
