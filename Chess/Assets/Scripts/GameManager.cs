using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardManager insBoardManager;
    GraphicManager insGraphicManager;
    PlayerManager insPlayerManager;

    [SerializeField] GameObject settingGame; 
    [SerializeField] bool smartAI = true;

    int? gameMode = null; // -- gamemode 1 = PVE // ---gamemode 2 = PVP
    int? colourSelected = null;  // -- colourSelected 1 = White // ---colourSelected 2 = Black
    



    void Start()
    {
        insBoardManager = FindObjectOfType<BoardManager>();
        insGraphicManager = FindObjectOfType<GraphicManager>();
        insPlayerManager = FindObjectOfType<PlayerManager>();
    }

    void StartGame()
    {
        settingGame.SetActive(false);

        insGraphicManager.Initiate();
        insPlayerManager.RequestPlayerInput(1);
        insBoardManager.StartGame((int)colourSelected);
    }

    public void LaunchGame()
    {
        if(gameMode != null || colourSelected != null)
        {
            StartGame();
        }
    }

    public void SelectedWhite()
    {
        colourSelected = 1;
    }

    public void SelectedBlack()
    {
        colourSelected = 2;
    }

    public void GameModePVE()
    {
        gameMode = 1;
    }

    public void GameModePVP()
    {
        gameMode = 2;
    }
}
