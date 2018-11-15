using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public enum GameState { MENU,GAME,GAMEOVER}
    public static GameState gameState;

    public snakeMouvement SM;
    public blockManager BM;

    public CanvasGroup MENU_CG;
    public CanvasGroup GAME_CG;
    public CanvasGroup GAMEOVER_CG;

    public GUIText scoreText;
    public GUIText MenuScoreText;
    public GUIText BestScoreText;
    public static int SCORE;
    public static int BESTSCORE;

    bool speedAdded;

    private void Start()
    {
        SetMenu();
        SCORE = 0;
        speedAdded = false;
        BESTSCORE = PlayerPrefs.GetInt("BESTSCORE");

    }

    private void Update()
    {
        scoreText.text = SCORE + "";
        MenuScoreText.text = SCORE + "";
        if(SCORE > BESTSCORE)
        {
            BESTSCORE = SCORE;
            BestScoreText.text = BESTSCORE + "";
            if(!speedAdded && SCORE > 150)
            {
                SM.speed++;
                speedAdded = true;
            }
        }

    }

    public void setMenu()
    {
        gameState = GameState.MENU;

        EnableCG(MENU_CG);
        DisableCG(GAME_CG);
        DisableCG(GAMEOVER_CG);
    }

    public void SetGameover()
    {
        gameState = GameState.GAMEOVER;
        EnableCG(MENU_CG);
        DisableCG(GAME_CG);
        DisableCG(GAMEOVER_CG);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Box"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bar"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SimpleBox"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Snake"))
        {
            Destroy(g);
        }
        SM.SpawnBodyPart();
        BM.SetPreviousPosAfterGameover();
        speedAdded = false;
        SM.speed = 3;
        PlayerPrefs.SetInt("BESTSCORE", BESTSCORE);
        BM.SimpleBoxPosition.Clear();

    }

    public void EnableCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;

    }
    public void DisableCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = false;
        cg.interactable = false;

    }
}
