using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoveset : MonoBehaviour
{
    BoardManager insBoardManager; 

    void Start()
    {
        insBoardManager = FindObjectOfType<BoardManager>();
    }

    public void CalculateMoves
        (
            string pieceType, Vector2 positionPiece, bool isInverted, bool isFirstMove,
            Vector2[] boardIndex, List<GameObject> pieceStorage, List<Vector3> possibleMoves,
            bool isWhite
        )

    {

        insBoardManager.DestroyMoves();

        if(pieceType.Equals("Pawn"))
        {
            if(!isInverted && isFirstMove)
            {   
                Vector3 lastMoveIndex = positionPiece;
                bool disableSpawn = false;
                bool disableNextSpawn = false;

                    //-- Pawn Moves 2 Slots if First Move on that piece (-Inverted-)
                for(int i = 0; i < 2; i++)
                {
                    lastMoveIndex.y++;

                    for(int a = 0; a < pieceStorage.Count; a++)
                    {
                        if(pieceStorage[a].GetComponent<PieceStorage>().GetIndexData().Equals
                            (lastMoveIndex))
                        {
                            if(pieceStorage[a].GetComponent<PieceStorage>().GetColourData()
                                .Equals(isWhite))
                            {
                                disableSpawn = true;
                            }
                            else if(pieceStorage[a].GetComponent<PieceStorage>()
                                .GetColourData() != (isWhite))
                            {
                                disableNextSpawn = true;
                            }
                        }
                    }

                    if(!disableSpawn)
                    {
                        for(int z = 0; z < boardIndex.Length; z++)
                        {
                            if(boardIndex[z].x.Equals(lastMoveIndex.x) &&
                                (boardIndex[z].y.Equals(lastMoveIndex.y)))
                            {
                                lastMoveIndex = new Vector3(lastMoveIndex.x,lastMoveIndex.y,z);

                                possibleMoves.Add(lastMoveIndex);
                            }
                        }

                        if(disableNextSpawn)
                        {
                            disableSpawn = true;
                        }
                    }
                }
                LeftRight(lastMoveIndex);
            }
            else if(!isInverted && !isFirstMove)
            {
                Vector3 lastMoveIndex = positionPiece;
                bool disableSpawn = false;
                bool disableNextSpawn = false;

                    //-- Pawn Moves 1 Slots if First Move on that piece (-Inverted-)
                for(int i = 0; i < 1; i++)
                {
                    lastMoveIndex.y++;

                    for(int a = 0; a < pieceStorage.Count; a++)
                    {
                        if(pieceStorage[a].GetComponent<PieceStorage>().GetIndexData().Equals
                            (lastMoveIndex))
                        {
                            if(pieceStorage[a].GetComponent<PieceStorage>().GetColourData()
                                .Equals(isWhite))
                            {
                                disableSpawn = true;
                            }
                            else if(pieceStorage[a].GetComponent<PieceStorage>()
                                .GetColourData() != (isWhite))
                            {
                                disableNextSpawn = true;
                            }
                        }
                    }
                    if(!disableSpawn)
                    {
                        for(int z = 0; z < boardIndex.Length; z++)
                        {
                            if(boardIndex[z].x.Equals(lastMoveIndex.x) &&
                                (boardIndex[z].y.Equals(lastMoveIndex.y)))
                            {
                                lastMoveIndex = new Vector3(lastMoveIndex.x,lastMoveIndex.y,z);

                                possibleMoves.Add(lastMoveIndex);
                            }
                        }

                        if(disableNextSpawn)
                        {
                            disableSpawn = true;
                        }
                    }
                }
                LeftRight(lastMoveIndex);
            }

            void LeftRight(Vector3 lastMoveIndex)
            {
                    // -- Right Kill --
                lastMoveIndex = positionPiece;
                lastMoveIndex = new Vector3(lastMoveIndex.x+1, lastMoveIndex.y+1,0);
                for(int i = 0; i < pieceStorage.Count; i ++)
                {
                    Vector2 pieceIndex = new Vector2(0,0);
                    pieceIndex = pieceStorage[i].GetComponent<PieceStorage>().GetIndexData();
                    
                    if(pieceStorage[i].GetComponent<PieceStorage>().GetColourData() &&
                        isWhite)
                    {
                        // - Target Friendly
                    }
                    else
                    {
                        if(pieceIndex.x.Equals(lastMoveIndex.x) &&
                            pieceIndex.y.Equals(lastMoveIndex.y))
                        {
                            for(int z = 0; z < boardIndex.Length; z++)
                            {
                                if(boardIndex[z].x.Equals(lastMoveIndex.x) &&
                                    (boardIndex[z].y.Equals(lastMoveIndex.y)))
                                {
                                    lastMoveIndex = new Vector3
                                        (lastMoveIndex.x,lastMoveIndex.y,z);
        
                                    possibleMoves.Add(lastMoveIndex);
                                }
                            }
                        }
                    }
                }

                    // -- Left Kill --
                lastMoveIndex = positionPiece;
                lastMoveIndex = new Vector3(lastMoveIndex.x-1, lastMoveIndex.y+1,0);
                for(int i = 0; i < pieceStorage.Count; i ++)
                {
                    Vector2 pieceIndex = new Vector2(0,0);
                    pieceIndex = pieceStorage[i].GetComponent<PieceStorage>().GetIndexData();
                    
                    if(pieceStorage[i].GetComponent<PieceStorage>().GetColourData() &&
                        isWhite)
                    {
                        // - Target Friendly
                    }
                    else
                    {
                        if(pieceIndex.x.Equals(lastMoveIndex.x) &&
                            pieceIndex.y.Equals(lastMoveIndex.y))
                        {
                            for(int z = 0; z < boardIndex.Length; z++)
                            {
                                if(boardIndex[z].x.Equals(lastMoveIndex.x) &&
                                    (boardIndex[z].y.Equals(lastMoveIndex.y)))
                                {
                                    lastMoveIndex = new Vector3
                                        (lastMoveIndex.x,lastMoveIndex.y,z);
        
                                    possibleMoves.Add(lastMoveIndex);
                                }
                            }
                        }
                    }
                }
            }
        }

        insBoardManager.CheckIfLegal(possibleMoves);
    }
}
