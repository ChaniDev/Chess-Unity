using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardManager insBoardManager;
    GraphicManager insGraphicManager;
    PlayerManager insPlayerManager;

    void Start()
    {
        insBoardManager = FindObjectOfType<BoardManager>();
        insGraphicManager = FindObjectOfType<GraphicManager>();
        insPlayerManager = FindObjectOfType<PlayerManager>();

        StartGame();
    }

    void StartGame()
    {
        insGraphicManager.Initiate();

        insPlayerManager.RequestPlayerInput(1);

        
    }

}
