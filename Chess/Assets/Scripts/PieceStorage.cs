using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceStorage : MonoBehaviour
{
    [SerializeField] Vector2 piecePosition = new Vector2(0,0);
    [SerializeField] Vector2 previousPosition = new Vector2(0,0);
    [SerializeField] bool isWhite = false;
    [SerializeField] bool isInverted = false;
    [SerializeField] bool isFirstMove = false;


        //-- Get Data --
    public Vector2 GetIndexData()
    {
        return (piecePosition);
    }

    public bool GetColourData()
    {
        return (isWhite);
    }

    public bool GetIfInverted()
    {
        return(isInverted);
    }

    public bool GetIfFirstMove()
    {
        return(isFirstMove);
    }

    public Vector2 GetPreviousIndex()
    {
        return(previousPosition);
    }


        //-- Set Data --
    public void SetIndexData(Vector2 indexData)
    {
        piecePosition.x = indexData.x;
        piecePosition.y = indexData.y;
    }

    public void SetIfInverted(bool ifInverted)
    {
        isInverted = ifInverted;
    }

    public void SetIfFirstMove(bool IfFirstMove)
    {
        isFirstMove = IfFirstMove;
    } 

    public void SetPreviousIndex()
    {
        previousPosition = piecePosition;
    }

    public void SetColourData(bool colourData)
    {
        isWhite = colourData;
    }
}
