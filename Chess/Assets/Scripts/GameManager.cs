using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardManager insBoardManager;
    GraphicManager insGraphicManager;

    void Start()
    {
        insBoardManager = FindObjectOfType<BoardManager>();
        insGraphicManager = FindObjectOfType<GraphicManager>();

        StartGame();
    }

    void StartGame()
    {
        insGraphicManager.Initiate();
    }

}
