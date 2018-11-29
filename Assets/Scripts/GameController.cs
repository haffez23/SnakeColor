using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public enum GameState{MENU, GAME, GAMEOVER};
	public static GameState gameState;

	[Header("Managers")]
	public SnakeMovement SM;
	public BlocksManager BM;

	[Header("Canvas Group")]
	public CanvasGroup Menu_CG;
	public CanvasGroup GAME_CG;
	public CanvasGroup GAMEOVER_CG;

	[Header("Score Management")]
	public Text ScoreText;
	public Text MenuScoreText;
	public Text BestScoreText;
	public static int SCORE;
	public static int BESTSCORE;

	[Header("SomeBool")]
	bool speedAdded;

	void Start(){
		SetMenu ();
		SCORE = 0;
		speedAdded = false;
		BESTSCORE = PlayerPrefs.GetInt ("BESTSCORE");
	}

	void Update(){
		ScoreText.text= SCORE + "";
		MenuScoreText.text= SCORE + "";

		if (SCORE > BESTSCORE) {
			BESTSCORE = SCORE;
			BestScoreText.text= BESTSCORE + "";
			if (!speedAdded && SCORE > 150) {
				SM.speed++;
				speedAdded = true;
			}
		}
	}

	public void SetMenu(){
		gameState = GameState.MENU;
		EnableCG (Menu_CG);
		DisableCG (GAME_CG);
		DisableCG (GAMEOVER_CG);

	}

	public void SetGame(){
		gameState = GameState.GAME;
		DisableCG (Menu_CG);
		EnableCG (GAME_CG);
		DisableCG (GAMEOVER_CG);

	}

	public void SetGameover(){
		gameState = GameState.GAMEOVER;
		DisableCG (Menu_CG);
		DisableCG (GAME_CG);
		EnableCG (GAMEOVER_CG);

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bar")) {
			Destroy (g);
		}

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("SimpleBox")) {
			Destroy (g);
		}

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Snake")) {
			Destroy (g);
		}

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Food")) {
			Destroy (g);
		}
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Box")) {
			Destroy (g);
		}

		SM.SpawnBodyPart ();
		BM.SetPreviousPosAfterGameOver ();

		speedAdded = false;
		SM.speed = 3;
		PlayerPrefs.SetInt ("BESTSCORE", BESTSCORE);
		BM.SimpleBoxPosition.Clear ();
	}

	public void EnableCG(CanvasGroup cg){
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}

	public void DisableCG(CanvasGroup cg){
		cg.alpha = 0;
		cg.interactable = false;
		cg.blocksRaycasts = false;
	}
}
