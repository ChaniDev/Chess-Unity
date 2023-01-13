using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceStorage : MonoBehaviour
{
    [SerializeField] Vector2 piecePosition = new Vector2(0,0);

    public Vector2 GetPosition()
    {
        return (piecePosition);
    }
}
