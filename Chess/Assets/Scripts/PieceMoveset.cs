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
            if(isFirstMove)
            {   
                Vector3 lastMoveIndex = positionPiece;
                bool disableSpawn = false;

                    //-- Pawn Moves 2 Slots if First Move on that piece (-)
                if(!isInverted)
                {
                    for(int i = 0; i < 2; i++)
                    {
                        lastMoveIndex.y++;
                        moveStraight(lastMoveIndex, disableSpawn);
                    }

                    LeftRight(lastMoveIndex, isInverted);
                }

                    //-- Pawn Moves 2 Slots if First Move on that piece (-)
                if(isInverted)
                {
                    for(int i = 0; i < 2; i++)
                    {
                        lastMoveIndex.y--;
                        moveStraight(lastMoveIndex, disableSpawn);
                    }

                    LeftRight(lastMoveIndex, isInverted);
                }
            }

            if(!isFirstMove)
            {
                Vector3 lastMoveIndex = positionPiece;
                bool disableSpawn = false;

                if(!isInverted)
                {
                        //-- Pawn Moves 1 Slots if First Move on that piece (-Inverted-)
                    for(int i = 0; i < 1; i++)
                    {
                        lastMoveIndex.y++;
                        moveStraight(lastMoveIndex, disableSpawn);
                    }
                    LeftRight(lastMoveIndex, isInverted);
                }

                if(isInverted)
                {
                        //-- Pawn Moves 1 Slots if First Move on that piece (-Inverted-)
                    for(int i = 0; i < 1; i++)
                    {
                        lastMoveIndex.y--;
                        moveStraight(lastMoveIndex, disableSpawn);
                    }
                    LeftRight(lastMoveIndex, isInverted);
                }
            }



            //-- Moves ---- Moves ---- Moves ---- Moves ---- Moves ---- Moves ---- Moves --//

            void moveStraight(Vector2 lastMoveIndex, bool disableSpawn)
            {
                if(lastMoveIndex.x > (7) || lastMoveIndex.y > (7) 
                        || lastMoveIndex.x < (0) || lastMoveIndex.y < (0))
                    {
                        return;
                    }

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
                            disableSpawn = true;
                        }
                    }
                }

                if(!disableSpawn)
                {
                    possibleMoves.Add(lastMoveIndex);        
                }
            }

            void LeftRight(Vector3 lastMoveIndex, bool isInverted)
            {
                if(!isInverted)
                {
                        // -- Right Kill --
                    lastMoveIndex = positionPiece;
                    lastMoveIndex = new Vector3(lastMoveIndex.x+1, lastMoveIndex.y+1,0);
                    
                    KillZone(lastMoveIndex);


                        // -- Left Kill --
                    lastMoveIndex = positionPiece;
                    lastMoveIndex = new Vector3(lastMoveIndex.x-1, lastMoveIndex.y+1,0);

                    KillZone(lastMoveIndex);
                }

                if(isInverted)
                {
                        // -- Right Kill --
                    lastMoveIndex = positionPiece;
                    lastMoveIndex = new Vector3(lastMoveIndex.x+1, lastMoveIndex.y-1,0);
                    
                    KillZone(lastMoveIndex);


                        // -- Left Kill --
                    lastMoveIndex = positionPiece;
                    lastMoveIndex = new Vector3(lastMoveIndex.x-1, lastMoveIndex.y-1,0);

                    KillZone(lastMoveIndex);
                }
            }

            void KillZone(Vector2 lastMoveIndex)
            {   
                if(lastMoveIndex.x > (7) || lastMoveIndex.y > (7) 
                        || lastMoveIndex.x < (0) || lastMoveIndex.y < (0))
                    {
                        return;
                    }

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
                            possibleMoves.Add(lastMoveIndex);        
                        }
                    }
                }
            }
        }


        insBoardManager.CheckIfLegal(possibleMoves);
    }
}
