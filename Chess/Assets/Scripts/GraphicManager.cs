using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    Vector3[] vectorLocation = new Vector3[64]; //--Total Slots--
    List<GameObject> movesHolder = new List<GameObject>();

    [SerializeField] private GameObject suggestedMove;

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
                vectorLocation[indexLocation].x = positionX;
                vectorLocation[indexLocation].y = positionY;
                vectorLocation[indexLocation].z = 0;
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

    public void SpawnMoves(int indexPosition)
    {
        if(indexPosition == 104)
        {
            for(int i = 0; i < movesHolder.Count; i++)
            {
                Destroy(movesHolder[i].gameObject);
            }
            movesHolder.Clear();
        }

        GameObject TEMP_move = Instantiate
            (suggestedMove, vectorLocation[indexPosition], Quaternion.identity);
    }
}
