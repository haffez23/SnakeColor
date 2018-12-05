using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int level=1;
    int nextlevel ;

	[Header("SomeBool")]
	bool speedAdded;


    [Header("Color parameters")]
    public GameObject textGameObject;
    private Text textColor;
    private Color color;
    public int levelLife;
    private readonly List<Color32> colors = new List<Color32>{ ColorList.red, ColorList.magenta, ColorList.blue , ColorList.green 
        ,ColorList.yellow, ColorList.cyan };
    private readonly List<string> colorsName = new List<string> { "RED", "MAGENTA", "BLUE", "GREEN", "YELLOW", "CYAN", "WHITE"};
    private int colorTimeChange = 0;
    public int changetime;
    bool playGame;



    void Start(){
        nextlevel = 0;
        playGame = false;
        textColor = textGameObject.GetComponent<Text>();
		SetMenu ();
		SCORE = 0;
		speedAdded = false;
		BESTSCORE = PlayerPrefs.GetInt ("BESTSCORE");
        InvokeColorChange();

    }


    public void InvokeColorChange()
    {
        Invoke("AddColorToTextColor", 0.1f);
    }

    void Update(){
        if(playGame)
        nextlevel++;
		ScoreText.text= SCORE + "";
		MenuScoreText.text= SCORE + "";

        if (Input.GetKey(KeyCode.D)/* || Input.GetTouch(0).phase == TouchPhase.Began*/)
        {
            SetGame();
            playGame = true;

        }
        colorTimeChange++;

        if (SCORE > BESTSCORE) {
			BESTSCORE = SCORE;
			BestScoreText.text= BESTSCORE + "";
			if (!speedAdded && SCORE > 150) {
				SM.speed++;
				speedAdded = true;
			}
		}
        if (nextlevel == levelLife)
        {
            switch (level)
            {
                case 1:
                    {
                        SM.GetComponent<SnakeMovement>().SetInitialAmount(SM.BodyParts.Count);
                        SceneManager.LoadScene("LEVEL2", LoadSceneMode.Single);
                        break;
                    }
                case 2:{
                        SM.GetComponent<SnakeMovement>().SetInitialAmount(SM.BodyParts.Count);
                        SceneManager.LoadScene("LEVEL3", LoadSceneMode.Single);
                        break;

                    }

                case 3:
                    SceneManager.LoadScene("LEVEL4", LoadSceneMode.Single);
                    break;
                case 4:
                    SceneManager.LoadScene("LEVEL1", LoadSceneMode.Single);
                    break;
                default:
                    SceneManager.LoadScene("LEVEL2", LoadSceneMode.Single);
                    break;

            }

        }
        if( colorTimeChange>changetime )
        {
            InvokeColorChange();
            colorTimeChange = 0;

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


    public void AddColorToTextColor()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Color32 thisImageColor = textColor.color;
            int randomIndex = Random.Range(0, colorsName.Count);
            textColor.text = colorsName[randomIndex];
            print(ColorList.getColor(textColor.text));
        if(currentScene.Equals("LEVEL3"))
            thisImageColor = ColorList.getColor(textColor.text);
        else if(currentScene.Equals("LEVEL4"))
        {
            int randomColor = Random.Range(0, colors.Count);
            thisImageColor = colors[randomColor];

        }
        textColor.color = thisImageColor;


    }
}
