using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlocksManager : MonoBehaviour {

	[Header("SnakeManager")]
	public SnakeMovement SM;
	public float distanceSnakeBarrier;

	[Header("Block Prefab")]
	public GameObject BlockPrefab;
    public GameObject BlockWithoutBarrierPrefab;

	[Header("Time to SpawnDelegate Management")]
    public float spawnTime;

	[Header("Snake Value for spawning")]
	public int minSpawnDist;
	Vector2 previousSnakePos;
	public List<Vector3> SimpleBoxPosition = new List<Vector3> ();


    private List<string> colorsName = new List<string> { "RED", "MAGENTA", "BLUE", "GREEN", "YELLOW", "CYAN", "WHITE" };
    private readonly int colorTimeChange = 0;
    private Vector3 prevSpawnBarrier;

    void Start(){
		SpawnBarrier ();
	}

	void Update(){
		if (GameController.gameState == GameController.GameState.GAME) {
           
           
			if (SM.transform.childCount > 0) {
				if (SM.transform.GetChild (0).position.y - previousSnakePos.y > minSpawnDist) {
                    SpawnBarrier ();
				}
			}
		}
	}

	public void SpawnBarrier(){
		float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
		float distBetweenBlocks = screenWidthWorldPos / 5;

        for (int i = -2; i < 3; i++)
        {
            float x = 2 * i * distBetweenBlocks;
            float y = 0;


            if (SM.transform.childCount > 0)
            {
                y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
                if (Screen.height / Screen.width == 4 / 3)
                {
                    y *= 4 / 3f;
                }
            }


            Vector3 spawnBarrier = new Vector3(x, y, 0);
            GameObject boxInstance;



                if (i == -2 || i == 2 || i == 0)
                {
                    boxInstance = Instantiate(BlockWithoutBarrierPrefab, spawnBarrier, Quaternion.identity, transform);

                }
                else
                {
                    boxInstance = Instantiate(BlockPrefab, spawnBarrier, Quaternion.identity, transform);

                }
           
            SpawnBlocks();

            prevSpawnBarrier = spawnBarrier;
            string currentScene = SceneManager.GetActiveScene().name;
            Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;



            if (currentScene.Equals("LEVEL1") || currentScene.Equals("LEVEL2"))
            {
                thisImageColor = new Color32(255, 0, 0, 255);

            }

            else 
            {


                for (int k = 0; k < colorsName.Count; k++)
                {
                    string temp = colorsName[k];
                    int randomIndex = Random.Range(k, colorsName.Count);
                    colorsName[k] = colorsName[randomIndex];
                    colorsName[randomIndex] = temp;
                    thisImageColor = ColorList.getColor(colorsName[randomIndex]);
                    boxInstance.name = temp;


                }

            }
            boxInstance.GetComponent<SpriteRenderer>().color = thisImageColor;






            if (SM.transform.childCount > 0) {
				previousSnakePos = SM.transform.GetChild (0).position;
			}
		}
	}


	public void SpawnBlocks(){
		float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
		float distBetweenBlocks = screenWidthWorldPos / 5;
		int random;
		random = Random.Range (-2, 3);
		float x = 2 * random * distBetweenBlocks;
		float y = 0;

		if (SM.transform.childCount > 0) {
			y = (int)SM.transform.GetChild (0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier ;
			if (Screen.height / Screen.width == 4 / 3) {
				y *= 2;
			}
		}

		Vector3 SpawnPos = new Vector3 (x, y, 0);
		bool canSpawnBlock = true;
		if (SimpleBoxPosition.Count == 0) {
			SimpleBoxPosition.Add (SpawnPos);
		} else {

            for (int k = 0; k < SimpleBoxPosition.Count; k++)
            {
                if (SpawnPos == SimpleBoxPosition[k] || prevSpawnBarrier == SimpleBoxPosition[k]  )   
                {
                    canSpawnBlock = false;
                }
            }

        }
		GameObject boxInstance;
		if (canSpawnBlock) {
			SimpleBoxPosition.Add (SpawnPos);
           
             
             boxInstance = Instantiate(BlockPrefab, SpawnPos, Quaternion.identity, transform);

           
			boxInstance.tag = "SimpleBox";
            string currentScene = SceneManager.GetActiveScene().name;
            Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;



            if (currentScene.Equals("LEVEL1") || currentScene.Equals("LEVEL2"))
            {
                thisImageColor = new Color32(255,0, 0, 255);

            }

            else 
            {

                  for (int k = 0; k < colorsName.Count; k++)
                {
                    string temp = colorsName[k];
                    int randomIndex = Random.Range(k, colorsName.Count);
                    colorsName[k] = colorsName[randomIndex];
                    colorsName[randomIndex] = temp;
                    thisImageColor = ColorList.getColor(colorsName[randomIndex]);
                    boxInstance.name = temp;


                }

            }
            boxInstance.GetComponent<SpriteRenderer>().color = thisImageColor;




            boxInstance.layer = LayerMask.NameToLayer ("Default");
			boxInstance.AddComponent<Rigidbody2D>();
			boxInstance.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

		}
	}

	public void SetPreviousPosAfterGameOver(){
		Invoke ("PreviousPosInvoke", 0.5f);
	}

	public void PreviousPosInvoke(){
		previousSnakePos.y = SM.transform.GetChild (0).position.y;
	}



}
