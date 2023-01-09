using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    Vector2[] vectorLocation = new Vector2[64]; //--Total Slots--

    void Start()
    {
        GenerateIndex(); // -- To generate an Index of Board -- 
    }

    void GenerateIndex()
    {
        float positionX = -3.5f;
        float positionY = -3.5f;

            //--!!-- Not Working as expected --!!--

        for(int i = 0; i < vectorLocation.Length; i++)
        {
            for(int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    vectorLocation[i] = new Vector2(positionX,positionY);
                    positionX++;
                }
                positionX = -3.5f;
                positionY++;
            }
        }

        print("DONE");
    }   

    public void MovePiece()
    {

    }
}
