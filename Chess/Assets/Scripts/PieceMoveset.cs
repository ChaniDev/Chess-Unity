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

            Vector3 lastMoveIndex = positionPiece;
            bool disableSpawn = false;

            if(isFirstMove)
            {   
                

                    //-- Pawn Moves 2 Slots if First Move on that piece (-)
                if(!isInverted)
                {
                    for(int i = 0; i < 2; i++)
                    {
                        lastMoveIndex.y++;
                        moveStraight();
                    }

                    LeftRight();
                }

                    //-- Pawn Moves 2 Slots if First Move on that piece (-)
                if(isInverted)
                {
                    for(int i = 0; i < 2; i++)
                    {
                        lastMoveIndex.y--;
                        moveStraight();
                    }

                    LeftRight();
                }
            }

            if(!isFirstMove)
            {
                if(!isInverted)
                {
                        //-- Pawn Moves 1 Slots if First Move on that piece (-Inverted-)
                    for(int i = 0; i < 1; i++)
                    {
                        lastMoveIndex.y++;
                        moveStraight();
                    }
                    LeftRight();
                }

                if(isInverted)
                {
                        //-- Pawn Moves 1 Slots if First Move on that piece (-Inverted-)
                    for(int i = 0; i < 1; i++)
                    {
                        lastMoveIndex.y--;
                        moveStraight();
                    }
                    LeftRight();
                }
            }



            //-- Moves ---- Moves ---- Moves ---- Moves ---- Moves ---- Moves ---- Moves --//

            void moveStraight()
            {
                bool isvalid = true;
                isvalid = OutOfBounds(lastMoveIndex);
                if(!isvalid)
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

            void LeftRight()
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
                bool isvalid = true;
                isvalid = OutOfBounds(lastMoveIndex);
                if(!isvalid)
                {
                    return;
                } 

                for(int i = 0; i < pieceStorage.Count; i ++)
                {
                    Vector2 pieceIndex = new Vector2(0,0);
                    pieceIndex = pieceStorage[i].GetComponent<PieceStorage>().GetIndexData();
                    
                    if(pieceStorage[i].GetComponent<PieceStorage>().GetColourData()
                        .Equals(isWhite))
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

        if(pieceType.Equals("Knight"))
        {
            Vector2[] moveSlots = new Vector2[8]
            {
                new Vector2(positionPiece.x-1,positionPiece.y+2),
                new Vector2(positionPiece.x+1,positionPiece.y+2),
                new Vector2(positionPiece.x-1,positionPiece.y-2),
                new Vector2(positionPiece.x+1,positionPiece.y-2),

                new Vector2(positionPiece.x+2,positionPiece.y-1),
                new Vector2(positionPiece.x+2,positionPiece.y+1),
                new Vector2(positionPiece.x-2,positionPiece.y-1),
                new Vector2(positionPiece.x-2,positionPiece.y+1),
            };

            for(int i = 0; i < moveSlots.Length; i++)
            {
                bool isvalid = true;
                isvalid = OutOfBounds(moveSlots[i]);

                if(isvalid)
                {
                    CheckIfFriendly(moveSlots[i]);
                }
            }

            void CheckIfFriendly(Vector2 moveIndex)
            {
                for(int i = 0; i < pieceStorage.Count; i++)
                {
                    if(pieceStorage[i].GetComponent<PieceStorage>().GetIndexData()
                        .Equals(moveIndex))
                    {
                        if(pieceStorage[i].GetComponent<PieceStorage>().GetColourData()
                            .Equals(isWhite))
                        {
                            // --Friendly
                        }
                        else
                        {
                            possibleMoves.Add(moveIndex);
                        }
                    }
                    else
                    {
                        possibleMoves.Add(moveIndex);
                    }
                }
            }
        }

        bool OutOfBounds(Vector2 lastMoveIndex)
        {
            if(lastMoveIndex.x > (7) || lastMoveIndex.y > (7) 
                || lastMoveIndex.x < (0) || lastMoveIndex.y < (0))
            {
                return(false);
            }

            return(true);
        }

        insBoardManager.CheckIfLegal(possibleMoves);
    }
}
