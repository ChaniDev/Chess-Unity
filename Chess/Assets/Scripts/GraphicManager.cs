using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    Vector2[] vectorLocation = new Vector2[64]; //--Total Slots--

    public void Initiate()
    {
        GenerateIndex(); // -- To generate an Index of Board -- 
    }

    void GenerateIndex()
    {
        float positionX = -3.5f;
        float positionY = -3.5f;
        int indexLocation = 0;

        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                vectorLocation[indexLocation] = new Vector2(positionX,positionY);
                indexLocation++;

                positionX++;
            }

            positionX = -3.5f;
            positionY++;
        }

        print("Board Vector Generated");
    }   

    public void MovePiece(int locationIndex)
    {

    }
}
