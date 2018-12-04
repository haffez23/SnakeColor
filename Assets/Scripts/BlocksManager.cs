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

    private  List<int> alpha = new List<int> { 0, 128,255 };


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

            if (currentScene.Equals("LEVEL3"))
            {
                Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;


                for (int k = 0; k < alpha.Count; k++)
                {
                    int temp = alpha[k];
                    int randomIndex = Random.Range(k, alpha.Count);
                    alpha[k] = alpha[randomIndex];
                    alpha[randomIndex] = temp;
                    thisImageColor = new Color32(255, (byte)alpha[k], 0, 255);

                }
                boxInstance.GetComponent<SpriteRenderer>().color = thisImageColor;

            }






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

            if(currentScene.Equals("LEVEL3"))
            {
                Color32 thisImageColor = boxInstance.GetComponent<SpriteRenderer>().color;


                for (int i = 0; i < alpha.Count; i++)
                {
                    int temp = alpha[i];
                    int randomIndex = Random.Range(i, alpha.Count);
                    alpha[i] = alpha[randomIndex];
                    alpha[randomIndex] = temp;
                    thisImageColor = new Color32(255, (byte)alpha[i], 0, 255);

                }
                boxInstance.GetComponent<SpriteRenderer>().color = thisImageColor;

            }



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
