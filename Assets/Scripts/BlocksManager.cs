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
	public float minSpawnTime;
	public float maxSpawnTime;
	private float thisTime;
	private float randomTime;
    public bool isBarrierExist;

	[Header("Snake Value for spawning")]
	public int minSpawnDist;
	Vector2 previousSnakePos;
	public List<Vector3> SimpleBoxPosition = new List<Vector3> ();


    private List<string> colorsName = new List<string> { "RED", "MAGENTA", "BLUE", "GREEN", "YELLOW", "CYAN", "WHITE" };
    private readonly int colorTimeChange = 0;

    void Start(){
		thisTime = 0;
		SpawnBarrier ();
		randomTime = Random.Range (minSpawnTime, maxSpawnTime);
	}

	void Update(){
		if (GameController.gameState == GameController.GameState.GAME) {
			if (thisTime < randomTime) {
				thisTime += Time.deltaTime;
			} else {
				SpawnBlocks ();
				thisTime = 0;
				randomTime = Random.Range (minSpawnTime, maxSpawnTime);
			}
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


            Vector3 SpawnPos = new Vector3(x, y, 0);
            GameObject boxInstance;


            if (isBarrierExist)
            {
                if (i == -2 || i == 2 || i == 0)
                {
                     boxInstance = Instantiate(BlockWithoutBarrierPrefab, SpawnPos, Quaternion.identity, transform);

                }
                else
                {
                     boxInstance = Instantiate(BlockPrefab, SpawnPos, Quaternion.identity, transform);

                }
            }
            else{
                 boxInstance = Instantiate(BlockWithoutBarrierPrefab, SpawnPos, Quaternion.identity, transform);

            }
            string currentScene = SceneManager.GetActiveScene().name;
            Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;



            if (currentScene.Equals("LEVEL1") || currentScene.Equals("LEVEL2"))
            {
                thisImageColor = new Color32(255, 0, 0, 255);

            }

            else if (currentScene.Equals("LEVEL3"))
            {


                for (int k = 0; k < colorsName.Count; k++)
                {
                    string temp = colorsName[k];
                    int randomIndex = Random.Range(k, colorsName.Count);
                    colorsName[k] = colorsName[randomIndex];
                    colorsName[randomIndex] = temp;
                    thisImageColor = ColorList.getColor(colorsName[randomIndex]);

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
			y = (int)SM.transform.GetChild (0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
			if (Screen.height / Screen.width == 4 / 3) {
				y *= 2;
			}
		}

		Vector3 SpawnPos = new Vector3 (x, y, 0);
		bool canSpawnBlock = true;
		if (SimpleBoxPosition.Count == 0) {
			SimpleBoxPosition.Add (SpawnPos);
		} else {
			for(int k= 0;k<SimpleBoxPosition.Count;k++){
                if (SpawnPos.x - distBetweenBlocks < SimpleBoxPosition[k].x && SpawnPos.x + distBetweenBlocks > SimpleBoxPosition[k].x)
                {
                    canSpawnBlock = false;
				}
			}
		}
		GameObject boxInstance;
		if (canSpawnBlock) {
			SimpleBoxPosition.Add (SpawnPos);
            if (isBarrierExist)
            {
                if (SimpleBoxPosition.Count % 2 != 0 || SimpleBoxPosition.Count == 1)
                {
                    boxInstance = Instantiate(BlockWithoutBarrierPrefab, SpawnPos, Quaternion.identity, transform);
                }
                else
                {
                    boxInstance = Instantiate(BlockPrefab, SpawnPos, Quaternion.identity, transform);

                }
            }
            else{
                boxInstance = Instantiate(BlockWithoutBarrierPrefab, SpawnPos, Quaternion.identity, transform);

            }
            boxInstance.name = "SimpleBox";
			boxInstance.tag = "SimpleBox";
            string currentScene = SceneManager.GetActiveScene().name;
            Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;



            if (currentScene.Equals("LEVEL1") || currentScene.Equals("LEVEL2"))
            {
                thisImageColor = new Color32(255,0, 0, 255);

            }

            else if (currentScene.Equals("LEVEL3"))
            {

                for (int k = 0; k < colorsName.Count; k++)
                {
                    string temp = colorsName[k];
                    int randomIndex = Random.Range(k, colorsName.Count);
                    colorsName[k] = colorsName[randomIndex];
                    colorsName[randomIndex] = temp;
                    thisImageColor = ColorList.getColor(colorsName[randomIndex]);

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
