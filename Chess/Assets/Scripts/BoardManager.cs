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
    int colourSelected = 0;


    public void StartGame(int insColourSelected)
    {
        insGraphicManager = FindObjectOfType<GraphicManager>();
        insPieceMoveset = FindObjectOfType<PieceMoveset>();

        colourSelected = insColourSelected;

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

    class Piece
    {
        public int indexID;
        public GameObject spawnPiece;
        public bool invertedMove = false;

        public void setData(int index, GameObject piece, bool inverted)
        {
            indexID = index;
            spawnPiece = piece;
            invertedMove = inverted;
        }
    }

    void InstantiatePiece()
    {
        int[] topArray = new int[]{48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63};
        int[] bottomArray = new int[]{8,9,10,11,12,13,14,15,0,1,2,3,4,5,6,7};

        GameObject[] blackPieceArray = new GameObject[]
        {
            blackPawn,blackPawn,blackPawn,blackPawn,blackPawn,blackPawn,blackPawn,blackPawn,
            blackRook,blackKnight,blackBishop,blackQueen,blackKing,blackBishop,blackKnight
            ,blackRook
        };

        GameObject[] whitePieceArray = new GameObject[]
        {
            whitePawn,whitePawn,whitePawn,whitePawn,whitePawn,whitePawn,whitePawn,whitePawn,
            whiteRook,whiteKnight,whiteBishop,whiteQueen,whiteKing,whiteBishop,whiteKnight
            ,whiteRook
        };

        List<Piece> PieceList = new List<Piece>();

        if(colourSelected == 1)
        {
            // -- White at bottom
            for(int i = 0; i < bottomArray.Length; i++)
            {
                Piece piece = new Piece();
                piece.setData(bottomArray[i],whitePieceArray[i], false);
                
                PieceList.Add(piece);
            }

            for(int i = 0; i < topArray.Length; i++)
            {
                Piece piece = new Piece();
                piece.setData(topArray[i],blackPieceArray[i], true);
                
                PieceList.Add(piece);
            }
        }
        else if(colourSelected == 2)
        {
            // -- Black at bottom
            for(int i = 0; i < bottomArray.Length; i++)
            {
                Piece piece = new Piece();
                piece.setData(bottomArray[i],blackPieceArray[i], false);
                
                PieceList.Add(piece);
            }

            for(int i = 0; i < topArray.Length; i++)
            {
                Piece piece = new Piece();
                piece.setData(topArray[i],whitePieceArray[i], true);
                
                PieceList.Add(piece);
            }
        }

        for(int i = 0; i < PieceList.Count; i++)
        {
            Vector3 vectorLocation = insGraphicManager
                .RequestVectorPosition(PieceList[i].indexID);
            //--Test index i --
            GameObject Piece = Instantiate
                (PieceList[i].spawnPiece, vectorLocation, Quaternion.identity);
            Piece.GetComponent<PieceStorage>().SetIndexData
                (boardIndex[PieceList[i].indexID]);
            Piece.GetComponent<PieceStorage>().SetIfInverted(PieceList[i].invertedMove);
            Piece.GetComponent<PieceStorage>().SetIfFirstMove(true);
            pieceStorage.Add(Piece);
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
            vectorLocation.z = 0;

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
