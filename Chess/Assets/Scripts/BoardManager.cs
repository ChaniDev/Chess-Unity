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
    [SerializeField] Transform whitePieces;
    [Space(20)]

    [SerializeField] GameObject whitePawn;
    [SerializeField] GameObject whiteKnight;
    [SerializeField] GameObject whiteBishop;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject whiteQueen;
    [SerializeField] GameObject whiteKing;

    [Space(20)]
    [SerializeField] Transform blackPieces;
    [Space(20)]
        // -- Black Pieces --
    [SerializeField] GameObject blackPawn;
    [SerializeField] GameObject blackKnight;
    [SerializeField] GameObject blackBishop;
    [SerializeField] GameObject blackRook;
    [SerializeField] GameObject blackQueen;
    [SerializeField] GameObject blackKing;

    [Space(20)]
    [SerializeField] Transform MoveStorage;

    [Space(20)]

        // -- Move GameObject
    [SerializeField] GameObject movePreview;

    [SerializeField] int? selectedPiece = null; 

    [Space(20)]

    [SerializeField] GameObject UpgradePrompt;
    bool pieceUpgrade = false;


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
        int[] whiteSpawnArray = new int[]{2+8+8+8+8+8, 4+8+8+8};

        foreach(int i in whiteSpawnArray)
        {
            Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(i);
                //--Test index i --
            GameObject Rook = Instantiate(whiteRook, vectorLocation, Quaternion.identity);
            Rook.GetComponent<PieceStorage>().SetIndexData(boardIndex[i]);
            Rook.GetComponent<PieceStorage>().SetIfInverted(false);
            Rook.GetComponent<PieceStorage>().SetIfFirstMove(false);
            pieceStorage.Add(Rook);
        }

        int[] blackSpawnArray = new int[]{2+8+8, 5+8+8+8+8};

        foreach(int i in blackSpawnArray)
        {
            Vector3 vectorLocation = insGraphicManager.RequestVectorPosition(i);
                //--Test index i --
            GameObject Rook = Instantiate(blackRook, vectorLocation, Quaternion.identity);
            Rook.GetComponent<PieceStorage>().SetIndexData(boardIndex[i]);
            Rook.GetComponent<PieceStorage>().SetIfInverted(true);
            Rook.GetComponent<PieceStorage>().SetIfFirstMove(false);
            pieceStorage.Add(Rook);
        }
    } 

    public void SelectPiece
        (
            string pieceType, Vector2 indexPosition, bool isInverted, 
            bool isFirstMove, bool isWhite, string upgradeName
        )
    {
        if(pieceType.Equals("Upgrade"))
        {
            if(upgradeName.Equals("Queen"))
            {
                UpgradePiece("Queen");

                UpgradePrompt.SetActive(false);
            }
            else if(upgradeName.Equals("Knight"))
            {
                UpgradePiece("Knight");

                UpgradePrompt.SetActive(false);
            }
        }

        if(pieceType.Equals("Board"))
        {
            DestroyMoves();

            if(pieceUpgrade)
            {
                ResetPosition();
            }
        }
        else if(pieceType.Equals("Move"))
        {
            if(pieceUpgrade)
            {                
                ResetPosition();
            }

            MovePiece(indexPosition);
        }
        else
        {
            if(pieceUpgrade)
            {
                UpgradePrompt.SetActive(false);
                
                ResetPosition();
                
            }

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

            GameObject move = Instantiate
                (movePreview, vectorLocation, Quaternion.identity, MoveStorage); 
            move.GetComponent<PieceStorage>().SetIndexData(possibleMoves[i]);
            moveStorage.Add(move);
        }
    }

    void MovePiece(Vector2 indexPosition)
    {
        Vector2 tmpIndexPosition = indexPosition;
        int? destroyIndex = null;

            if(selectedPiece.Equals(null))
            {
                DestroyMoves();
                return;
            }

            pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
                .SetPreviousIndex();
            
            pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
                .SetIfFirstMove(false);

            
            for(int i = 0; i < pieceStorage.Count; i++)
            {
                if(pieceStorage[i].GetComponent<PieceStorage>().GetIndexData()
                    .Equals(indexPosition))
                {
                    Destroy(pieceStorage[i]);
                    destroyIndex = i;
                    break;
                }
            }

            pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
                .SetIndexData(tmpIndexPosition);
            
            if(pieceStorage[(int)selectedPiece].gameObject.tag.Equals("Pawn"))
            {
                Vector2 pieceIndex = pieceStorage[(int)selectedPiece]
                    .GetComponent<PieceStorage>().GetIndexData();
                
                if(pieceIndex.y.Equals(7) || pieceIndex.y.Equals(0))
                {
                    pieceUpgrade = true;
                    UpgradePrompt.SetActive(true);
                }
            }

            Vector3 vectorPosition = new Vector3(0,0,0);

            for(int i = 0; i < boardIndex.Length; i++)
            {
                if(boardIndex[i].Equals(tmpIndexPosition))
                {
                    vectorPosition = insGraphicManager.RequestVectorPosition(i);
                }
            }

            pieceStorage[(int)selectedPiece].transform.position = vectorPosition;

            if(destroyIndex.Equals(null))
            {
                //--Do Nothing--//
            }
            else
            {
                pieceStorage.RemoveAt((int)destroyIndex);
            }

            DestroyMoves();
            // !!! -- Scan the board for check -- !!!
    } 

    void UpgradePiece(string upgradeName)
    {
        bool tmpColour = pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
            .GetColourData();

        bool tmpIsInverted = pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
            .GetIfInverted();

        Vector2 tmpIndexPosition = pieceStorage[(int)selectedPiece]
            .GetComponent<PieceStorage>().GetIndexData();

        Vector3 tmpVectorData = new Vector3(0,0,0);

        for(int i = 0; i < boardIndex.Length; i++)
        {
            if(tmpIndexPosition.Equals(boardIndex[i]))
            {
                tmpVectorData = insGraphicManager.RequestVectorPosition(i);
            }
        }

        Destroy(pieceStorage[(int)selectedPiece]);
        pieceStorage.RemoveAt((int)selectedPiece);

        if(upgradeName.Equals("Queen"))
        {
            if(tmpColour) // --White
            {
                GameObject Queen = Instantiate
                    (whiteQueen, tmpVectorData, Quaternion.identity, whitePieces);
                Queen.GetComponent<PieceStorage>().SetIfInverted(tmpIsInverted);
                Queen.GetComponent<PieceStorage>().SetColourData(tmpColour);
                Queen.GetComponent<PieceStorage>().SetIndexData(tmpIndexPosition);

                pieceStorage.Add(Queen);
            }
            else    // --Black
            {
                GameObject Queen = Instantiate
                    (blackQueen, tmpVectorData, Quaternion.identity, blackPieces);
                Queen.GetComponent<PieceStorage>().SetIfInverted(tmpIsInverted);
                Queen.GetComponent<PieceStorage>().SetColourData(tmpColour);
                Queen.GetComponent<PieceStorage>().SetIndexData(tmpIndexPosition);

                pieceStorage.Add(Queen);
            }
        }
        else if(upgradeName.Equals("Knight"))
        {
            if(tmpColour) // --White
            {
                GameObject Knight = Instantiate
                    (whiteKnight, tmpVectorData, Quaternion.identity, whitePieces);
                Knight.GetComponent<PieceStorage>().SetIfInverted(tmpIsInverted);
                Knight.GetComponent<PieceStorage>().SetColourData(tmpColour);
                Knight.GetComponent<PieceStorage>().SetIndexData(tmpIndexPosition);

                pieceStorage.Add(Knight);
            }
            else    // --Black
            {
                GameObject Knight = Instantiate
                    (blackKnight, tmpVectorData, Quaternion.identity, blackPieces);
                Knight.GetComponent<PieceStorage>().SetIfInverted(tmpIsInverted);
                Knight.GetComponent<PieceStorage>().SetColourData(tmpColour);
                Knight.GetComponent<PieceStorage>().SetIndexData(tmpIndexPosition);

                pieceStorage.Add(Knight);
            }
        }

        pieceUpgrade = false;

        print("Upgrade");
        selectedPiece = null;
    }

    void ResetPosition()
    {
        Vector2 tmpIndexPosition = new Vector2(0,0);
        Vector3 tmpVectorPosition = new Vector3(0,0,0);

        tmpIndexPosition = pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
            .GetPreviousIndex();

        for(int i = 0; i < boardIndex.Length; i++)
        {
            if(tmpIndexPosition.Equals(boardIndex[i]))
            {
                tmpVectorPosition = insGraphicManager.RequestVectorPosition(i);
            }
        }

        pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
            .SetIndexData(tmpIndexPosition);

        pieceStorage[(int)selectedPiece].GetComponent<PieceStorage>()
            .SetPreviousIndex();

        pieceStorage[(int)selectedPiece].gameObject.transform.position = tmpVectorPosition;

        print("Reset");

        UpgradePrompt.SetActive(false);
        selectedPiece = null;
        pieceUpgrade = false;

        return;
    }
}
